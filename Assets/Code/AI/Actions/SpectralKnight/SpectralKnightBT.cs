using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviourTree;
using Bunny;
using Bunny.Tools;

public class SpectralKnightBT: BTTree
{
    public LayerMask playerMask;

    Action<BunnyMessage<float>> onAttackedCallback;

    protected override BTNode SetupTree()
    {
        onAttackedCallback = OnAttacked; 
        BunnyEventManager.Instance.RegisterEvent("OnSpectralKnightDied", this);
        BunnyEventManager.Instance.RegisterEvent("DamageBossRequest", this);
        BunnyEventManager.Instance.OnEventRaised<float>("DamageBossRequest", onAttackedCallback);

        BTNode root = new BTSelector(new List<BTNode>
        {
            new SBDeath(this),
            new BTSequence(new List<BTNode>
            {
                new BTCheckEnragedState(this),
                new BTFacePlayer(this),
                new BTCheckMeleeAttackTimer(this),
                new BTMoveTowardsPlayer(this),
                new BTCheckMeleeRange(this),
                new SKEnragedAttack(this),
            }),
            new BTSequence(new List<BTNode>
            {
                new BTCheckMeleeAttackTimer(this),
                new BTFacePlayer(this),
                new BTMoveTowardsPlayer(this),
                new BTCheckMeleeRange(this),
                new BTMeleeAttack(this),
            }),
            new BTSequence(new List<BTNode>
            {
                new BTFacePlayer(this),
            }),
        });
        return root;
    }

    protected override void OnUpdate()
    {
    }

    public void OnAttacked(BunnyMessage<float> message) {
        if (Health <= 0) return;
        Health -= message.payload;
        BunnyEventManager.Instance.Fire<float>("OnBossHurt", new BunnyMessage<float>(Health, this));

        if (Health <= GeneralData.Health * GeneralData.EnragePoint) {
            Enraged = true;
        }

        if (Health <= 0) {
            BunnyEventManager.Instance.Fire<bool>("OnSpectralKnightDied", new BunnyMessage<bool>(true, this));
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