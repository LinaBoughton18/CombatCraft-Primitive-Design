using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tracks which state an enemy is currently in
// Not a monobehaviour, doesn't sit on one specific object or anything
// Other classes will inherit from it

public class EnemyStateMachine
{
    // A reference to what our current state is
    public EnemyState currentEnemyState { get; set; }

    // Called when we create an enemy
    // NOT automatically called, we need to call it when instantiating
    public void Initialize(EnemyState startingState)
    {
        currentEnemyState = startingState;
        currentEnemyState.EnterState();
    }
    
    // Changes from one state to another by exiting, reassigning, and entering
    public void ChangeState(EnemyState newState)
    {
        currentEnemyState.ExitState();
        currentEnemyState = newState;
        currentEnemyState.EnterState();
    }

}
