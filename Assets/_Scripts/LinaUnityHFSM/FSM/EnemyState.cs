using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
By LlamaAcademy!
This file contains an enumeration of type EnemyState
It's basically a limited list of states that are possible for an enemy to have

To reference one of these states, we use EnemyState.Idle (or .Chase, etc.)
Lists the names of all the various states that are possible
for any enemy to have.

*/



namespace LlamAcademy.FSM
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Roll,
        Spit,
        Bounce,
        Die,
        Patrol
    }
}