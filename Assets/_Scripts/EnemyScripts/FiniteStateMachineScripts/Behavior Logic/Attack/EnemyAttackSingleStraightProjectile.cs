using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lets us create an asset menu in our scene
[CreateAssetMenu(fileName = "Attack-Single-Stright-Projectile", menuName = "EnemyLogic/Attack Loic/Single-Straight-Projectile Chase")]
// Individual logic file (can be subbed out for others, I think)
public class EnemyAttackSingleStraightProjectile : EnemyAttackSOBase
{
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float timeBetweenShots = 2f;
    [SerializeField] private float timeTillExit = 3f;
    [SerializeField] private float distanceToCountExit = 3f;
    [SerializeField] private float bulletSpeed = 10f;

    private float timer;
    private float exitTimer;

    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        // make it so the enemy can move to the player

        // When we're ready to shoot the enemy
        if (timer > timeBetweenShots)
        {
            // resets the time between shots to 0
            timer = 0f;

            // sets the direction of the projectile
            Vector2 dir = (playerTransform.position - enemyBase.transform.position).normalized;

            Rigidbody2D bullet = GameObject.Instantiate(bulletPrefab, enemyBase.transform.position, Quaternion.identity);
            bullet.velocity = dir * bulletSpeed;
        }

        if (Vector2.Distance(playerTransform.position, enemyBase.transform.position) < distanceToCountExit)
        {
            exitTimer += Time.deltaTime;

            if (exitTimer > timeTillExit)
            {
                enemyBase.stateMachine.ChangeState(enemyBase.chaseState);
            }
        }
        else
        {
            exitTimer = 0f;
        }
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
