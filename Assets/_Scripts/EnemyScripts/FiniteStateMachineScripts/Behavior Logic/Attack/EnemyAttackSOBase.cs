using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scriptable Objects are ways to create "tiny little asset files in the project"
// that usually store data of some kind
// For scriptable objects, changes made in play mode stay changed
// (which doesn't normally happen)
// However, we're just storing our logic in here, not data
public class EnemyAttackSOBase : ScriptableObject
{
    protected EnemyBase enemyBase;
    // Since this isn't a monobehaviour, we don't automatically have
    // transform & gameobject, so we need to set those up here
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    // Instead of state, for SO's we use initialize
    public virtual void Initialize(GameObject gameObject, EnemyBase enemyBase)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemyBase = enemyBase;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic()
    {
        // For some reason I don't entirely understand, we're not baking a state
        // change into the attack state, we're letting individual state logic
        // handle that. (whatever that means)
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(EnemyBase.AnimationTriggerType triggerType) { }
    // If you need to reset any bools or anything when you exit a state,
    // you can do that here
    public virtual void ResetValues() { }
}