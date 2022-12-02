using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Tools;
using AI.BehaviourTree;

namespace AI
{
    public abstract class EnemyAction : MonoBehaviour
    {
        protected Rigidbody2D body;
        protected Animator animator;
        protected BunnyEventManager eventManager;

        public virtual void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            animator = gameObject.GetComponentInChildren<Animator>();
            eventManager = BunnyEventManager.Instance;
        }
    }
}