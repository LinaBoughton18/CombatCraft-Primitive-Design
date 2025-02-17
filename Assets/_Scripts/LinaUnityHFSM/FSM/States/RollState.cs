using UnityHFSM;
using System;
using UnityEngine;

namespace LlamAcademy.FSM
{
    // Roll: rolls directly forward (in 3D, might not work in 2D)
    // for the duration of ExitTime (3f)
    public class RollState : EnemyStateBase
    {
        public RollState(
            bool needsExitTime,
            Enemy Enemy,
            Action<State<EnemyState, StateEvent>> onEnter,
            float ExitTime = 3f) : base(needsExitTime, Enemy, ExitTime, onEnter) { }

        public override void OnEnter()
        {
            Debug.Log("Roll");
            Agent.isStopped = true;
            base.OnEnter();
            //Animator.Play("Roll"); // CAN DELETE
        }

        public override void OnLogic()
        {
            Agent.Move(1.5f * Agent.speed * Time.deltaTime * Agent.transform.forward);
            base.OnLogic();
        }
    }
}
