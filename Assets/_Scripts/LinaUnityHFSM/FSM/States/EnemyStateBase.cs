using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityHFSM;

/*
BY LLAMA ACADEMY!
Derrives from UnityHFSM, extends the class State from UnityHFSM
Groups common behavior that ALL the various states will use
*/

namespace LlamAcademy.FSM
{
    public abstract class EnemyStateBase : State<EnemyState, StateEvent>
    {
        // Common fields shared by all states (such as the concrete Enemy or NavMeshAgent)
        protected readonly Enemy Enemy;
        protected readonly NavMeshAgent Agent;
        protected readonly Animator Animator;
        // Ways to defer exiting the state
        protected bool RequestedExit;
        protected float ExitTime;

        protected readonly Action<State<EnemyState, StateEvent>> onEnter;
        protected readonly Action<State<EnemyState, StateEvent>> onLogic;
        protected readonly Action<State<EnemyState, StateEvent>> onExit;
        protected readonly Func<State<EnemyState, StateEvent>, bool> canExit;

        // Constructor: setting the Enemy type, receive the onEnter & onExit functions,
        // & configuration related to the exit time (needsExitTime, ExitTime, & canExit)
        public EnemyStateBase(bool needsExitTime,
            Enemy Enemy,
            float ExitTime = 0.1f,
            Action<State<EnemyState, StateEvent>> onEnter = null,
            Action<State<EnemyState, StateEvent>> onLogic = null,
            Action<State<EnemyState, StateEvent>> onExit = null,
            Func<State<EnemyState, StateEvent>, bool> canExit = null)
        {
            this.Enemy = Enemy;
            this.onEnter = onEnter;
            this.onLogic = onLogic;
            this.onExit = onExit;
            this.canExit = canExit;
            this.ExitTime = ExitTime;
            this.needsExitTime = needsExitTime;

            Agent = Enemy.GetComponent<NavMeshAgent>();
            Animator = Enemy.GetComponent<Animator>();
        }

        // Called whenever we transition to this state
        public override void OnEnter()
        {
            base.OnEnter();
            RequestedExit = false;
            onEnter?.Invoke(this); // ? assigns stuff to null if there happens to be a null, error handling basically
        }

        // Called every tick, executes state logic
        public override void OnLogic()
        {
            base.OnLogic();
            if (RequestedExit && timer.Elapsed >= ExitTime)
            {
                fsm.StateCanExit();
            }
        }

        // Called whenever the state machine wants to exit the state,
        // but there's some Exit Time or some other condition
        // that's preventing it from immediately exiting.
        public override void OnExitRequest()
        {
            // Both outcomes of this if/else will lead to StateCanExit being called,
            // it's just being called in different locations (here vs in OnLogic)

            // If we don't need exit time OR canExit is true
            if (!needsExitTime || canExit != null && canExit(this))
            {
                // Performs a transition (if needed) & calls OnExit
                fsm.StateCanExit();
            }
            else
            {
                // Tracks whether we've called OnExitRequest or not
                // This sends information to OnLogic that will automatically exit
                // once the necessary time has elapsed.
                RequestedExit = true;
            }

            // States like AttackStates will use the exit time (in case they're
            // in the middle of an attack)
        }
    }
}