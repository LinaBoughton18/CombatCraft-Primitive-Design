using UnityEngine;
using UnityHFSM;
using LlamAcademy.Sensors;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;

/*
By LlamaAcademy!
Provides data to the state machine, makes the decisions,
& houses the conditions & states.

In the video, since Llama is the only enemy type, this serves
as both base & concrete class.
Fields like PlayerController Player, are useful references for every enemy,
so we can keep them here.
However, fields like SpitPrefab are specific to the enemy class, so we can define
a child class that derrives from Enemy, call it "LlamaEnemy (or something).

*/

namespace LlamAcademy.FSM
{
    // NOTE: This class requires an animator, but I don't have animations currently
    // I have commented out the animator bit for now, but I can add it back later!
    //[RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class Enemy : MonoBehaviour
    {
        // Variables that the various states will be referencing
        [Header("Generic Enemy References")]
        [SerializeField]
        protected PlayerController Player;
        [SerializeField]
        public Vector3 spawnPoint;
        [SerializeField]
        public float speed;

        // Enemy components
        protected StateMachine<EnemyState, StateEvent> EnemyFSM;
        //private Animator Animator; // I'm not using an animator right now
        protected NavMeshAgent Agent;

        protected void Awake()
        {
            // Finds the player & creates a reference to them    // LINA ADDED
            Player = GameObject.Find("Player").GetComponent<PlayerController>();
            
            // Assigns components
            Agent = GetComponent<NavMeshAgent>();
            Agent.updateRotation = false; // Prevents unwanted rotation
            Agent.updateUpAxis = false;   // Ensures the agent moves properly in 2D

            //Animator = GetComponent<Animator>(); // I'm not using an animator right now

            // Assigns spawn point to initial spawn location
            spawnPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            // Creates an instance of the StateMachine for the class to reference
            EnemyFSM = new();

            // Lets the child class set up its states & transitions in an override function
            // LINA ADDITION :)
            AddStatesAndTransitions();

            // Initializes the state machine
            EnemyFSM.Init();
        }

        private void Update()
        {
            // Updates FSM logic every frame
            EnemyFSM.OnLogic();
        }

        // A function overridden in each instance of the enemy child classes, declares the states & transitions
        protected abstract void AddStatesAndTransitions();
    }
}
