using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Tools;

namespace AI.BehaviourTree {
    public abstract class BTTree : MonoBehaviour
    {

        protected Rigidbody2D body;
        protected Animator animator;
        protected BunnyEventManager eventManager;
        private BTNode _root = null;

        public virtual void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            animator = gameObject.GetComponentInChildren<Animator>();
            eventManager = BunnyEventManager.Instance;
        }

        protected void Start()
        {
            Debug.Log("Called start");
            _root = SetupTree();
        }

        private void Update()
        {
            Debug.Log($"Updating {_root != null}");
            if (_root != null)
                _root.Evaluate();
        }

        protected abstract BTNode SetupTree();
    }
}
