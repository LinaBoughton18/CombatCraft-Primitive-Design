using UnityHFSM;
using System;
using UnityEngine;

namespace LlamAcademy.FSM
{
    public class BounceState : EnemyStateBase
    {
        //private ParticleSystem BounceParticleSystem; // CAN DELETE

        public BounceState(
            bool needsExitTime,
            Enemy Enemy,
            //ParticleSystem BounceParticleSystem, // CAN DELETE
            Action<State<EnemyState, StateEvent>> onEnter,
            float ExitTime = 0.33f) : base(needsExitTime, Enemy, ExitTime, onEnter)
        {
            //this.BounceParticleSystem = BounceParticleSystem; // CAN DELETE
        }

        public override void OnEnter()
        {
            Debug.Log("Bounce");
            Agent.isStopped = true;
            base.OnEnter();
            //Animator.Play("Bounce"); // CAN DELETE
            //BounceParticleSystem.Play(); // CAN DELETE
        }
    }
}
