using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviourTree;
using Bunny;
using Bunny.Tools;

public class DragonEyeBT: BTTree
{

    public BoxCollider2D physicsCollider;

    public LayerMask playerMask;

    Action<BunnyMessage<float>> onAttackedCallback;

    // Special 1
    public GameObject DEProjectile;

    protected override BTNode SetupTree()
    {
        body = GetComponentInParent<Rigidbody2D>();
        physicsCollider = transform.parent.gameObject.GetComponent<BoxCollider2D>();

        onAttackedCallback = OnAttacked; 
        BunnyEventManager.Instance.RegisterEvent("OnDragonEyeDied", this);
        BunnyEventManager.Instance.RegisterEvent("DamageBossRequest", this);
        BunnyEventManager.Instance.OnEventRaised<float>("DamageBossRequest", onAttackedCallback);

        body.velocity = new Vector2(GeneralData.RunningSpeed, GeneralData.RunningSpeed);

        BTNode root = new BTSelector(new List<BTNode>
        {
            new DEDeath(this),
            new DEShoot(this),
        });
        return root;
    }

    protected override void OnUpdate()
    {
        if (Health <= GeneralData.Health * GeneralData.EnragePoint) {
            Enraged = true;
        }
    }

    public void OnAttacked(BunnyMessage<float> message) {
        if (Health <= 0) return;
        Health -= message.payload;
        BunnyEventManager.Instance.Fire<float>("OnBossHurt", new BunnyMessage<float>(Health, this));

        if (Health <= 0) {
            BunnyEventManager.Instance.Fire<bool>("OnDragonEyeDied", new BunnyMessage<bool>(true, this));
            Health = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && CollisionAttacking)
        {
            BunnyEventManager.Instance.Fire<float>("DamagePlayerRequest", new BunnyMessage<float>(10f, this));
        }
    }
}
