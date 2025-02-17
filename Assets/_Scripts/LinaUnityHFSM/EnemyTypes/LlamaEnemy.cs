using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LlamAcademy.FSM;
using LlamAcademy;
using LlamAcademy.Sensors;
using Unity.VisualScripting;
using UnityHFSM;

public class LlamaEnemy : Enemy
{
    #region ENEMY SPECIFIC FIELDS/VARIABLES
    // Variables specific to the enemy type that the various states will be referencing
    [Header("Llama Specific References")]
    [SerializeField]
    private Spit SpitPrefab;
    [SerializeField]
    private ParticleSystem BounceImpactParticleSystem;

    // Set up cooldowns & ranges for our enemy
    [Header("Attack Config")]
    [SerializeField]
    [Range(0.1f, 5f)] // This gives us a slider we can use in the Unity editor!
    private float AttackCooldown = 2;
    [SerializeField]
    [Range(1, 20f)]
    private float RollCooldown = 17;
    [SerializeField]
    [Range(1, 10f)]
    private float BounceCooldown = 10;

    #endregion

    #region ENEMY SPECIFIC SENSORS & RANGE BOOLEANS
    // Of the class type PlayerSensor, which sense players
    // Used for the ranges (triggers) required for various states
    [Header("Sensors")]
    [SerializeField]
    private PlayerSensor FollowPlayerSensor;
    [SerializeField]
    private PlayerSensor RangeAttackPlayerSensor;
    [SerializeField]
    private PlayerSensor MeleePlayerSensor;
    [SerializeField]
    private ImpactSensor RollImpactSensor;

    // Booleans to keep track of the sensor statuses
    // Confirm that we are in range for the proper state
    [Space]
    [Header("Debug Info")]
    [SerializeField]
    private bool IsInMeleeRange;
    [SerializeField]
    private bool IsInSpitRange;
    [SerializeField]
    private bool IsInChasingRange;
    [SerializeField]
    private float LastAttackTime;
    [SerializeField]
    private float LastBounceTime;
    [SerializeField]
    private float LastRollTime;

    #endregion

    // Called by the parent class in Awake()
    protected override void AddStatesAndTransitions()
    {
        #region SET UP STATES

        // Add States (Concrete information! - different for each enemy!)
        // The llama has these 6 states
        EnemyFSM.AddState(EnemyState.Idle, new IdleState(false, this));
        EnemyFSM.AddState(EnemyState.Chase, new ChaseState(true, this, Player.transform));
        EnemyFSM.AddState(EnemyState.Spit, new SpitState(true, this, SpitPrefab, OnAttack));
        EnemyFSM.AddState(EnemyState.Bounce, new BounceState(true, this, /*BounceImpactParticleSystem,*/ OnBounce));
        EnemyFSM.AddState(EnemyState.Roll, new RollState(true, this, OnRoll));
        EnemyFSM.AddState(EnemyState.Attack, new AttackState(true, this, OnAttack));

        // Sets the start state to idle (by default, the first state you add is set to idle)
        EnemyFSM.SetStartState(EnemyState.Idle);

        #endregion

        #region SET UP TRANSITIONS

        // Add Transitions (Concrete information! - different for each enemy!)

        // AddTransition, a transition between two states, works like this:
        // AddTransition(new Transition(from, to, condition));

        // TriggerTransitions are called when there is a specific trigger (like an event!)

        // TriggerTransitions will trigger a transition between states!
        // TriggerTransitions fire only when one of these state events is raised

        // Idle to/from Chase Transitions
        EnemyFSM.AddTriggerTransition(StateEvent.DetectPlayer, new Transition<EnemyState>(EnemyState.Idle, EnemyState.Chase));
        EnemyFSM.AddTriggerTransition(StateEvent.LostPlayer, new Transition<EnemyState>(EnemyState.Chase, EnemyState.Idle));
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.Chase,
            (transition) => IsInChasingRange
                            && Vector3.Distance(Player.transform.position, transform.position) > Agent.stoppingDistance)
        );
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.Idle,
            (transition) => !IsInChasingRange
                            || Vector3.Distance(Player.transform.position, transform.position) <= Agent.stoppingDistance)
        );
            
        // Roll Transitions
        // In the video (20:45) adding true to the end of some of these forced an instant transition
        // However, the update to HFSM seems to break this???
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.Roll, ShouldRoll));
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.Roll, ShouldRoll));
        EnemyFSM.AddTriggerTransition(StateEvent.RollImpact,
            new Transition<EnemyState>(EnemyState.Roll, EnemyState.Bounce, null));
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Roll, EnemyState.Chase, IsNotWithinIdleRange));
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Roll, EnemyState.Idle, IsWithinIdleRange));
            
        // Bounce transitions
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.Bounce, ShouldBounce));
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.Bounce, ShouldBounce));
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Bounce, EnemyState.Chase, IsNotWithinIdleRange));
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Bounce, EnemyState.Idle, IsWithinIdleRange));
            
        // Spit Transitions
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.Spit, ShouldSpit));
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.Spit, ShouldSpit));
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Spit, EnemyState.Chase, IsNotWithinIdleRange));
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Spit, EnemyState.Idle, IsWithinIdleRange));
            
        // Attack Transitions
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Chase, EnemyState.Attack, ShouldMelee));
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Idle, EnemyState.Attack, ShouldMelee));
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Attack, EnemyState.Chase, IsNotWithinIdleRange));
        EnemyFSM.AddTransition(new Transition<EnemyState>(EnemyState.Attack, EnemyState.Idle, IsWithinIdleRange));

        #endregion

    }

    // Sets up sensors
    protected void Start()
    {
        // Assigning events to the values of the sensors
        FollowPlayerSensor.OnPlayerEnter += FollowPlayerSensor_OnPlayerEnter;
        FollowPlayerSensor.OnPlayerExit += FollowPlayerSensor_OnPlayerExit;
        RangeAttackPlayerSensor.OnPlayerEnter += RangeAttackPlayerSensor_OnPlayerEnter;
        RangeAttackPlayerSensor.OnPlayerExit += RangeAttackPlayerSensor_OnPlayerExit;
        MeleePlayerSensor.OnPlayerEnter += MeleePlayerSensor_OnPlayerEnter;
        MeleePlayerSensor.OnPlayerExit += MeleePlayerSensor_OnPlayerExit;
        RollImpactSensor.OnCollision += RollImpactSensor_OnCollision;
    }

    // A collider is used to detect when the llama collides with something during its roll, which will end the roll
    // Raises an event that will 
    private void RollImpactSensor_OnCollision(Collision2D Collision)
    {
        EnemyFSM.Trigger(StateEvent.RollImpact);
        LastRollTime = Time.time;
        LastAttackTime = Time.time;
        RollImpactSensor.gameObject.SetActive(false);
    }

    // FOR ALL BELOW!
    // Discussed at 19:00 in the video!
    // To my understanding, everything here with the void keyword are all event handlers
    // They are just setting the proper boolean variable, telling us that the player is in the proper range
    // & some of them are raising a trigger event in addition.

    private void FollowPlayerSensor_OnPlayerExit(Vector3 LastKnownPosition)
    {
        EnemyFSM.Trigger(StateEvent.LostPlayer);
        IsInChasingRange = false;
    }

    private void FollowPlayerSensor_OnPlayerEnter(Transform Player)
    {
        EnemyFSM.Trigger(StateEvent.DetectPlayer);
        IsInChasingRange = true;
    }

    #region SHOULD______ (STATE TRANSITIONS -- ATTACK COOLDOWNS)
    // All these "Should____'s" are keeping track of attack cooldowns

    private bool ShouldRoll(Transition<EnemyState> Transition) =>
        LastRollTime + RollCooldown <= Time.time
               && IsInChasingRange;

    private bool ShouldBounce(Transition<EnemyState> Transition) =>
        LastBounceTime + BounceCooldown <= Time.time
               && IsInMeleeRange;

    private bool ShouldSpit(Transition<EnemyState> Transition) =>
        LastAttackTime + AttackCooldown <= Time.time
               && !IsInMeleeRange
               && IsInSpitRange;

    private bool ShouldMelee(Transition<EnemyState> Transition) =>
        LastAttackTime + AttackCooldown <= Time.time
               && IsInMeleeRange;

    #endregion

    private bool IsWithinIdleRange(Transition<EnemyState> Transition) =>
        Agent.remainingDistance <= Agent.stoppingDistance;

    private bool IsNotWithinIdleRange(Transition<EnemyState> Transition) =>
        !IsWithinIdleRange(Transition);

    private void MeleePlayerSensor_OnPlayerExit(Vector3 LastKnownPosition) => IsInMeleeRange = false;

    private void MeleePlayerSensor_OnPlayerEnter(Transform Player) => IsInMeleeRange = true;

    private void RangeAttackPlayerSensor_OnPlayerExit(Vector3 LastKnownPosition) => IsInSpitRange = false;

    private void RangeAttackPlayerSensor_OnPlayerEnter(Transform Player) => IsInSpitRange = true;

    #region ON_____STATE (POSSIBLE ENTER FUNCTIONS????????????????????????????)

    // These functions are called when the state is entered (I think)
    private void OnAttack(State<EnemyState, StateEvent> State)
    {
        //transform.LookAt(Player.transform.position);
        LastAttackTime = Time.time;
    }

    private void OnBounce(State<EnemyState, StateEvent> State)
    {
        //transform.LookAt(Player.transform.position);
        LastAttackTime = Time.time;
        LastBounceTime = Time.time;
    }

    // Set impact sensor (not important to Lina),
    // look at player (for direction),
    // and set the time of our last attack to now (for cooldown purposes)
    private void OnRoll(State<EnemyState, StateEvent> State)
    {
        RollImpactSensor.gameObject.SetActive(true);
        //transform.LookAt(Player.transform.position);
        LastRollTime = Time.time;
    }

    #endregion
}
