using UnityEngine;

namespace LlamAcademy.FSM
{
    public class ChaseState : EnemyStateBase
    {
        // Chase: enemy chases the player

        private Transform Target;

        // Construtor: requires sending the player through as a target
        public ChaseState(bool needsExitTime, Enemy Enemy, Transform Target) : base(needsExitTime, Enemy) 
        {
            this.Target = Target;
        }

        public override void OnEnter()
        {
            Debug.Log("Chase");
            base.OnEnter();
            Agent.enabled = true; // Enables the NavMeshAgent, allowing the enemy to follow the player
            Agent.isStopped = false;
            //Animator.Play("Walk"); // CAN DELETE
        }

        public override void OnLogic()
        {
            base.OnLogic();
            // If we have not yet requested to exit, 
            if (!RequestedExit)
            {
                // you can add a more complex movement prediction algorithm like what 
                // we did in AI Series 44: https://youtu.be/1Jkg8cKLsC0
                Agent.SetDestination(Target.position);
            }
            // Makes it so the enemy will walk to the player's last known location & then return to idle
            // LINA WILL MAYBE CHANGE THIS TO HAVE THE ENEMY RETURN TO SPAWN INSTEAD!
            else if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                // In case that we were requested to exit, we will continue moving to the last known position prior to transitioning out to idle.
                fsm.StateCanExit();
            }
        }
    }
}
