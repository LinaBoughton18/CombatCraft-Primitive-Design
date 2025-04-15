using UnityHFSM;
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace LlamAcademy.FSM
{
    public class SpitState : EnemyStateBase
    {
        private Spit Prefab;
        private ObjectPool<Spit> Pool;
        // What is an objectPool?
        // Has something to do with shooting lots of times & having lots of llamas?
        // I probably don't need this?

        // Constructor
        public SpitState(
            bool needsExitTime, 
            Enemy Enemy, 
            Spit Prefab, 
            Action<State<EnemyState, StateEvent>> onEnter,
            float ExitTime = 0.33f) : base(needsExitTime, Enemy, ExitTime, onEnter)
        {
            this.Prefab = Prefab;
            Pool = new(CreateObject, GetObject, ReleaseObject);
        }

        // IDK WHAT THE FOLLOWING THREE FUNCTIONS ARE DOING TBH
        // POSSIBLY SOMETHING TO DO WITH SPIT PARTICLE EFFECTS BUT IDK
        private Spit CreateObject() // Unsure what this does
        {
            return GameObject.Instantiate(Prefab);
        }

        private void GetObject(Spit Instance) // Unsure what this does
        {
            Instance.transform.forward = Enemy.transform.forward;
            Instance.transform.position = Enemy.transform.position + Enemy.transform.forward + Vector3.up * 1.5f;
            Instance.gameObject.SetActive(true);
        }

        private void ReleaseObject(Spit Instance) // Unsure what this does
        {
            Instance.gameObject.SetActive(false);
        }

        public override void OnEnter()
        {
            Debug.Log("Spit");
            Agent.isStopped = true;
            base.OnEnter();
            //Animator.Play("Attack"); // CAN DELETE
            Pool.Get();
        }
    }
}
