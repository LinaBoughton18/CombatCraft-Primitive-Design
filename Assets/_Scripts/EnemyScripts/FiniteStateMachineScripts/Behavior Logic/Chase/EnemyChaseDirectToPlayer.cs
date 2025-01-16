using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lets us create an asset menu in our scene
[CreateAssetMenu(fileName = "Chase-Direct-To-Player Chase", menuName = "EnemyLogic/Chase Loic/Direct Chase")]
// Individual logic file (can be subbed out for others, I think)
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{
    [SerializeField] private float movementSpeed = 1.75f;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Hello from the ChaseDirectToPlayerState");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        // In the FNS video, he uses this function to then move the enemy to the player
        // However, since I'm using a pathfinding script, I don't need to do that

        /* In the video, this section is deleted and not replaced
        // Also however, we do want to check if the player is in range of the next trigger
        // So we'll check for that
        if (enemyBase.isWithinStrikingDistance)
        {
            enemyBase.stateMachine.ChangeState(enemyBase.attackState);
        }
        */
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        base.Initialize(gameObject, enemyBase);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
