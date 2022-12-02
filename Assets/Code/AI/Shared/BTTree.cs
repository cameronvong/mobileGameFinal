using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Tools;

namespace AI.BehaviourTree {
    public abstract class BTTree : MonoBehaviour
    {

        public Rigidbody2D body;
        public Animator animator;
        public BunnyEventManager eventManager;
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
            OnUpdate();
        }

        protected abstract void OnUpdate();

        protected abstract BTNode SetupTree();
    }
}
