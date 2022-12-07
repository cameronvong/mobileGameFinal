using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviourTree;
using Bunny;
using Bunny.Tools;

public class DragonEyeBT: BTTree
{
    public AIData BossData;
    public float SpecialTimer;
    public float DefaultAttackTimer;

    public Player target;
    public LayerMask playerMask;

    Action<BunnyMessage<float>> onAttackedCallback;

    public bool CollisionAttacking = false;

    // Stats
    public float Health;

    // Special 1
    public GameObject SkullProjectile;

    protected override BTNode SetupTree()
    {
        Health = BossData.Health;
        SpecialTimer = 0f;
        DefaultAttackTimer = 0f;

        body.velocity = new Vector2(10f, 10f);

        onAttackedCallback = OnAttacked; 
        BunnyEventManager.Instance.RegisterEvent("OnSkullBossDied", this);
        BunnyEventManager.Instance.OnEventRaised<float>("DamageBossRequest", onAttackedCallback);

        BTNode root = new BTSelector(new List<BTNode>
        {
            
        });
        return root;
    }

    protected override void OnUpdate()
    {
        SpecialTimer += Time.deltaTime;
        DefaultAttackTimer += Time.deltaTime;
    }

    public void OnAttacked(BunnyMessage<float> message) {
        if (Health <= 0) return;
        Health -= message.payload;
        BunnyEventManager.Instance.Fire<float>("OnBossHurt", new BunnyMessage<float>(Health, this));

        if (Health <= 0) {
            BunnyEventManager.Instance.Fire<bool>("OnSkullBossDied", new BunnyMessage<bool>(true, this));
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