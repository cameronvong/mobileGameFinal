using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    // Later need to make this able to run on its own thread
    // pfft, may go with AI state machines -- moe
    public abstract class EnemyAction : MonoBehaviour
    {
        protected Rigidbody2D body;
        protected Animator animator;
        // Maybe we can just change the below into a damage event
        // protected PlayerController player; -- NotImplemented

        public virtual void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            animator = gameObject.GetComponentInChildren<Animator>();
        }

        public abstract void Execute();

    }
}