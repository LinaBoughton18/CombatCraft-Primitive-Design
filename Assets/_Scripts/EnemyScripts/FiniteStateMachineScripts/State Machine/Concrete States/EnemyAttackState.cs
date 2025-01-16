using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {

    }

    // Overrides of EnemyState, control the distinct ways we enter,
    // exit, frame update, and physics update
    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        enemyBase.EnemyAttackBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Hello from the attack state");

        enemyBase.EnemyAttackBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        enemyBase.EnemyAttackBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemyBase.EnemyAttackBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemyBase.EnemyAttackBaseInstance.DoPhysicsLogic();
    }
}
