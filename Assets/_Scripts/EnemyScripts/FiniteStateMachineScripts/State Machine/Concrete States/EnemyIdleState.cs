using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Because this isn't a monobehavior, an instance of this class isn't created automatically
// Instead, we'll create instances of it in EnemyBase, so now these scripts
// will be generated when we instantiate an instance of EnemyBase (by spawning in an Enemy)

public class EnemyIdleState : EnemyState
{
    
    public EnemyIdleState(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {

    }

    // Overrides of EnemyState, control the distinct ways we enter,
    // exit, frame update, and physics update
    public override void AnimationTriggerEvent(EnemyBase.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);

        enemyBase.EnemyIdleBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        enemyBase.EnemyIdleBaseInstance.DoEnterLogic();
    }

    public override void ExitState()
    {
        base.ExitState();

        enemyBase.EnemyIdleBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemyBase.EnemyIdleBaseInstance.DoFrameUpdateLogic();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemyBase.EnemyIdleBaseInstance.DoPhysicsLogic();
    }
}