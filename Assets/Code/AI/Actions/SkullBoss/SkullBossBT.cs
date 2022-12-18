using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviourTree;
using Bunny;
using Bunny.Tools;

public class SkullBossBT: BTTree
{
    public LayerMask playerMask;

    Action<BunnyMessage<float>> onAttackedCallback;

    protected override BTNode SetupTree()
    {
        body.velocity = new Vector2(10f, 10f);

        onAttackedCallback = OnAttacked; 
        BunnyEventManager.Instance.RegisterEvent("OnSkullBossDied", this);
        BunnyEventManager.Instance.RegisterEvent("DamageBossRequest", this);
        BunnyEventManager.Instance.OnEventRaised<float>("DamageBossRequest", onAttackedCallback);

        BTNode root = new BTSelector(new List<BTNode>
        {
            new SBDeath(this),
            new BTSequence(new List<BTNode>
            {
                new BTCheckMeleeAttackTimer(this),
                new BTFacePlayer(this),
                new BTMoveTowardsPlayer(this),
                new BTCheckMeleeRange(this),
                new BTMeleeAttack(this),
            }),
        });
        return root;
    }

    protected override void OnUpdate()
    {
    }

    public void OnAttacked(BunnyMessage<float> message) {
        if (Health <= 0) return;
        if(this.flashEffect != null) this.flashEffect.Flash(Color.red);
        Health -= message.payload;
        BunnyEventManager.Instance.Fire<float>("OnBossHurt", new BunnyMessage<float>(Health, this));

        if (Health <= GeneralData.Health * GeneralData.EnragePoint) {
            Enraged = true;
        }

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