using UnityHFSM;
using System;
using UnityEngine;

namespace LlamAcademy.FSM
{
    // Attack: stop moving, perform the attack,
    // ExitTime automatically triggers? (sending us back to ChaseState)
    public class AttackState : EnemyStateBase
    {
        public AttackState(
            bool needsExitTime,
            Enemy Enemy,
            Action<State<EnemyState, StateEvent>> onEnter,
            float ExitTime = 0.33f) : base(needsExitTime, Enemy, ExitTime, onEnter) {}

        public override void OnEnter()
        {
            Debug.Log("Attack");
            Agent.isStopped = true;
            base.OnEnter();
            //Animator.Play("Attack"); // CAN DELETE
        }
    }
}
