using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Tools;

namespace AI.BehaviourTree {
    public abstract class BTTree : MonoBehaviour
    {

        public Rigidbody2D body;
        public AIData GeneralData;
        public Animator animator;
        public SpriteRenderer spriteRenderer;
        public BoxCollider2D mainCollider;
        public ColoredFlash flashEffect;

        public BunnyEventManager eventManager;

        public Player target;

        private BTNode _root = null;

        // Timers
        public float SpecialAttackTimer;
        public float MeleeAttackTimer;
        public float RangeAttackTimer;

        public float Health;

        public bool CollisionAttacking = false;
        public bool Enraged = false;
        

        public virtual void Awake()
        {
            eventManager = BunnyEventManager.Instance;
        }

        protected void Start()
        {
            Debug.Log("Called start");
            Health = GeneralData.Health;
            body = GetComponent<Rigidbody2D>();
            mainCollider = GetComponent<BoxCollider2D>();
            animator = gameObject.GetComponentInChildren<Animator>();
            spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
            this.flashEffect = gameObject.GetComponentInChildren<ColoredFlash>();
            GameObject plyrObj = GameObject.FindWithTag("Player");
            if(plyrObj != null) target = plyrObj.GetComponent<Player>();
            _root = SetupTree();
        }

        private void Update()
        {
            Debug.Log($"Updating {_root != null}");
            if (_root != null)
                _root.Evaluate();

            SpecialAttackTimer += Time.deltaTime;
            MeleeAttackTimer += Time.deltaTime;
            RangeAttackTimer += Time.deltaTime;
            OnUpdate();
        }

        protected abstract void OnUpdate();

        protected abstract BTNode SetupTree();
    }
}
