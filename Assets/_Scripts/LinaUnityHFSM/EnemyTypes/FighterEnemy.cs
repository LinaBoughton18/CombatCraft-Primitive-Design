using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LlamAcademy.FSM;
using LlamAcademy;
using LlamAcademy.Sensors;
using Unity.VisualScripting;
using UnityHFSM;

public class FighterEnemy : Enemy
{
    #region ENEMY SPECIFIC FIELDS/VARIABLES
    // Variables specific to the enemy type that the various states will be referencing
    [Header("Fighter Specific References")]
    // For patrol state
    [SerializeField]
    private float patrolRadius;
    [SerializeField]
    private float stoppingDistance;

    #endregion

    protected override void AddStatesAndTransitions()
    {
        #region SET UP STATES

        EnemyFSM.AddState(EnemyState.Patrol, new PatrolState(false, this, patrolRadius, stoppingDistance));
        EnemyFSM.SetStartState(EnemyState.Patrol);

        #endregion

        #region SET UP TRANSITIONS



        #endregion
    }
}
