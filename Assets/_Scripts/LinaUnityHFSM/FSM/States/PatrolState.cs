using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace LlamAcademy.FSM
{
    // Patrol: enemy patrols their spawn point
    public class PatrolState : EnemyStateBase
    {
        private Vector3 randomPatrolPoint; // The random point we will patrol to
        private readonly float patrolRadius; // The radius from our spawn point we can patrol in // MAY GET MOVED TO SPECIFIC ENEMY??
        private readonly float stoppingDistance; // The distance we have to be from a patrol point to stop pathing there // MAY GET MOVED TO SPECIFIC ENEMY??

        //TEMPORARY VARIABLES FOR HOLDING PATROL NAVMESH SPEED, ETC.
        private float tempSpeed = 1.3f;

        // A constructor for PatrolState: needsExitTime = do we need exit time or nah?
        // Enemy is the Enemy instance we're using
        public PatrolState(bool needsExitTime, Enemy Enemy, float patrolRadius, float stoppingDistance) : base(needsExitTime, Enemy)
        {
            this.stoppingDistance = stoppingDistance;
            this.patrolRadius = patrolRadius;
            // Automatically sets the Enemy & patrolRadius due to the base function (I think)
        }

        public override void OnEnter()
        {
            Debug.Log("Patrol");
            base.OnEnter();

            // Activates the agent
            Agent.enabled = true;
            Agent.isStopped = false;

            // Temporarily slows down speed
            Agent.speed = tempSpeed;

            // Selects a starting patrol point
            SelectRandomPatrolPoint();
        }

        public override void OnLogic()
        {
            base.OnLogic();

            // We shall travel to our patrol point
            // Once we have reached our patrol point, select a new random patrol point
            if (!Agent.pathPending && Agent.remainingDistance <= stoppingDistance)
            {
                SelectRandomPatrolPoint();
            }
        }

        public void SelectRandomPatrolPoint()
        {
            // Calculate new random patrol point
            Vector3 randomDirection = Random.insideUnitCircle * patrolRadius;
            //randomDirection.z = randomDirection.y; // Fix for 2D
            randomDirection.z = 0; // Keep it on the same plane
            Vector3 newPatrolPoint = Enemy.spawnPoint + randomDirection;

            // Validate point on NavMesh & sets the proper point, otherwise, try again!
            if (NavMesh.SamplePosition(newPatrolPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                randomPatrolPoint = hit.position; // Translates the point into a valid NavMesh point
                Agent.SetDestination(randomPatrolPoint); // Sets destination
            }
            else
            {
                // Retry if the point is invalid
                SelectRandomPatrolPoint();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            // Sets speed back to normal!
            Agent.speed = Enemy.speed;
        }
    }
}