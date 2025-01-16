using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    // IDK where to put this destinationSetter tbh,
    // probably in idle or enemyChaseDirectToPlayer
    //private AIDestinationSetter destinationSetter;
    //public AIDestinationSetter = GetComponent<AIDestinationSetter>;

    public EnemyChaseState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {
        //destinationSetter = GetComponent<AIDestinationSetter>();
    }

    // Overrides of EnemyState, control the distinct ways we enter,
    // exit, frame update, and physics update
    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        enemyBase.EnemyChaseBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Hello from the chase state, we need to edit the target here");
        // We need to grab a reference to the AIDestinationSetter script and make it set itself as target, then the player

        enemyBase.EnemyChaseBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        enemyBase.EnemyChaseBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemyBase.EnemyChaseBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemyBase.EnemyChaseBaseInstance.DoPhysicsLogic();
    }
}
