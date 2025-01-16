using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The base class for all enemy states
// The base class for the states, not the enemies themselves, that's in enemybase

// NOT a monobehaviour, but why?
public class EnemyState
{
    // We want to access our enemyBase class & enemyStateMachine
    // while we're in the states
    protected EnemyBase enemyBase;
    protected EnemyStateMachine enemyStateMachine;
    // Because these are protected, outside classes cannot access them,
    // but inheriting classes can access them :)

    // Constructor for an instance of this script
    public EnemyState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine)
    {
        this.enemyBase = enemyBase;
        this.enemyStateMachine = enemyStateMachine;
    }

    // We must consider: what logic will we need? States are self-contained,
    // They hold the logic within them. We'll need to include logic like
    // "when do we enter/exit this state?", "what do we do in this state?", etc.
    // We will create virtual methods, where we can overwrite them in inherieted classes
    // Unlike abstract methods, where we're forced to write them into a class


    // These are just possible functions useful for building an enemy
    // I could add in OnCollisionEnter functions or the like, depending on what
    // I need for state changes
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType) { }
}
