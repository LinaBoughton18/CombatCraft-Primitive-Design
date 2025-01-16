using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The intention is for this script to be applied to every enemy in the game
// We will have differing scripts for every enemy type (maybe going down in levels of specificty)

public class EnemyBase : MonoBehaviour, IDamagable, IEnemyMovable, ITriggerCheckable
{
    // IDamagable Health variables
    [field: SerializeField]
    public float maxHealth { get; set; } = 100f;
    public float currentHealth { get; set; }

    // IEnemyMovable Variables
    public Rigidbody2D enemyRigidbody { get; set; }
    public bool isFacingRight { get; set; } = true;

    // Movement Variable
    public AIDestinationSetter targetSetter;

    // Instances of the various states we need for the enemy
    #region State Machine Variables

    public EnemyStateMachine stateMachine { get; set; }
    public EnemyIdleState idleState { get; set; }
    public EnemyChaseState chaseState { get; set; }
    public EnemyAttackState attackState { get; set; }

    #endregion

    #region ScritableObject Variables

    // Serialized so that we can plug them in, in the inspector
    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;

    // With these, we're referencing instances of scriptable objects,
    // which is important so that our enemies are all referencing
    // unique instances of the scriptable objects!
    // We instantiate instances of these scriptable objects in Awake()
    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }

    #endregion

    //-----FUNCTIONS-----//
    private void Awake()
    {
        // Instantiating instances of our scriptable object logic
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);
        
        // Instantiate and set up instances of our various enemy states
        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);

        targetSetter = GetComponent<AIDestinationSetter>();
    }

    void Start()
    {
        currentHealth = maxHealth;

        enemyRigidbody = GetComponent<Rigidbody2D>();

        // Initialize (different than instantiate, which is done in awake()),
        // instances of our scriptable objects
        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);


        // initialize the state machine with starting state idle
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.currentEnemyState.FrameUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.currentEnemyState.PhysicsUpdate();
    }

    #region Damage Functions

    // IDamagable: Take damage & die if necessary
    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // IDamagable: Destroy self
    public void Die()
    {
        // Destroys self
        Destroy(gameObject);
    }

    #endregion

    #region Movement Functions

    // IEnemyMovable: 
    // Implementing my own version integrated with A* project
    // When ready to move, set the target to the player
    // Implementation in the video is at 4:30
    public void MoveEnemy(Vector2 velocity)
    {
        targetSetter.target = GameObject.Find("Player").transform;
    }

    // IEnemyMovable: Video implementation at 4:30, don't think I need it but who knows
    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        
    }

    #endregion

    #region Animation Triggers

    // Plays certain animations on triggers
    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        // Thanks to this, in our animation window, when we add an animation, we can
        // add a trigger & perform logic if necessary
        stateMachine.currentEnemyState.AnimationTriggerEvent(triggerType);
    }

    // Triggers for finite state machine?
    public enum AnimationTriggerType
    {
        enemyDamaged,
        playFootstepsSound
    }

    #endregion

    // ITriggerCheckable variables
    #region ITriggerCheckable Variables & Distance Checks

    public bool isAggroed { get; set; }
    public bool isWithinStrikingDistance { get; set; }

    public void SetAggroStatus(bool isAggroed)
    {
        this.isAggroed = isAggroed;
    }

    public void SetStrikingDistanceBool(bool isWithinStrikignDistance)
    {
        this.isWithinStrikingDistance = isWithinStrikignDistance;
    }
    #endregion

}