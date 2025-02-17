using UnityEngine;

namespace LlamAcademy.FSM
{
    // Idle(still): enemy stands still
    public class IdleState : EnemyStateBase
    {
        // Tracks animation repetitiveness (LINA DOES NOT NEED THIS)
        //private float AnimationLoopCount = 0;

        // A constructor for IdleState: needsExitTime = do we need exit time or nah?
        // Enemy is the Enemy instance we're using
        public IdleState(bool needsExitTime, Enemy Enemy) : base(needsExitTime, Enemy) { }

        public override void OnEnter()
        {
            Debug.Log("Idle");
            base.OnEnter();
            Agent.isStopped = true; // deactivates the NavMeshAgent, holding the enemy still
            // LINA DOES NOT NEED THIS
            //Animator.Play("Idle_A");
        }

        public override void OnLogic()
        {
            #region LLAMA ANIMATIONS (LINA DOES NOT NEED THIS - CAN BE DELETED!)
            /*
            AnimatorStateInfo state = Animator.GetCurrentAnimatorStateInfo(0);

            if (state.normalizedTime >= AnimationLoopCount + 1)
            {
                float value = Random.value;
                if (value < 0.95f)
                {
                    if (!state.IsName("Idle_A"))
                    {
                        AnimationLoopCount = 0;
                    }
                    else
                    {
                        AnimationLoopCount++;
                    }
                    Animator.Play("Idle_A");
                }
                else if (value < 0.975f)
                {
                    if (!state.IsName("Idle_B"))
                    {
                        AnimationLoopCount = 0;
                    }
                    else
                    {
                        AnimationLoopCount++;
                    }
                    Animator.Play("Idle_B");
                }
                else
                {
                    if (!state.IsName("Idle_C"))
                    {
                        AnimationLoopCount = 0;
                    }
                    else
                    {
                        AnimationLoopCount++;
                    }
                    Animator.Play("Idle_C");
                }
            }
            */
            #endregion

            // base.___ (where ___ is a function) calls the parent class's behavior
            // when you have a function that is being overridden!
            // You can call base.___ before or after your custom logic!
            base.OnLogic();
        }
    }
}