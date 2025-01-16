using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lets us create an asset menu in our scene
[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "EnemyLogic/Idle Loic/Random Wander")]
// Individual logic file (can be subbed out for others, I think)
public class EnemyIdleRandomWander : EnemyIdleSOBase
{
    // Stole these over from EnemyBase script
    [SerializeField] public float RandomMovementRange = 5f;
    [SerializeField] public float RandomMovementSpeed = 1f;
    // Stole these from EnemyIdleState script
    private Vector3 targetPos;
    private Vector3 direction;

    // So these functions are our base functions, and now we can edit them
    // To suit the individual needs of our logic
    public override void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("Hello from EnemyIdleRandomWander");

        targetPos = GetRandomPointInCircle();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        // There used to be logic here dictating that if isAggroed == true,
        // then we'd ChangeState to chase, but now we don't need to do that anymore!

        // In the video we calculate the direction the enemy needs to move in
        // and use that to move to the randomPointInCircle we got from entering this state
        // But I can just set randomPointInCircle = to the target point for A*

        // Once we reach our target point, grab a random point in the circle
        // And continue wandering
        if ((enemyBase.transform.position - targetPos).sqrMagnitude < 0.01f)
        {
            targetPos = GetRandomPointInCircle();
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

    // Gives us a random point in the circle around the enemy
    private Vector3 GetRandomPointInCircle()
    {
        /*
        // Find closest walkable node
        var startNode = AstarPath.active.GetNearest(transform.position, NNConstraint.Walkable).node;
        var nodes = PathUtilities.BFS(startNode, nodeDistance);
        var singleRandomPoint = PathUtilities.GetPointsOnNodes(nodes, 1)[0];
        //var multipleRandomPoints = PathUtilities.GetPointsOnNodes(nodes, 100);  This line isn't really needed I think

        return singleRandomPoint;
        */

        // leftover from the FNS machine vide
       return enemyBase.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * RandomMovementRange;
        
    }
}
