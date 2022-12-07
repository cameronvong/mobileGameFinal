using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviourTree;
using Bunny;
using Bunny.Tools;

public class SwordBossBT: BTTree
{
    public AIData SwordBossData;
    public float SpecialTimer;
    public float DefaultAttackTimer;

    public LayerMask playerMask;

    Action<BunnyMessage<float>> onAttackedCallback;

    // Special 1
    public Collider2D SpawnAreaCollider;
    public GameObject FallingSwordPrefab;
    public GameObject SACGO;

    protected override BTNode SetupTree()
    {
        SwordBossBT bossInstance = this;
        Health = SwordBossData.Health;
        SpecialTimer = 0f;
        DefaultAttackTimer = 0f;
        SpawnAreaCollider = SACGO.GetComponent<BoxCollider2D>();

        onAttackedCallback = OnAttacked; 
        BunnyEventManager.Instance.RegisterEvent("OnSwordBossDied", this);
        BunnyEventManager.Instance.OnEventRaised<float>("DamageBossRequest", onAttackedCallback);

        BTNode root = new BTSelector(new List<BTNode>
        {
            new SwordBossDeath(bossInstance),
            new BTSequence(new List<BTNode>
            {
                new SBDefaultAttackCheck(bossInstance),
                new BTSequence(new List<BTNode>
                {
                    new SwordBossMoveTowardsPlayer(bossInstance),
                    new SBPlunge(bossInstance),
                    new SwordBossGroundStuck(bossInstance),
                }),
            }),
            new BTSequence(new List<BTNode>
            {
                new SwordBossSummonMinion(bossInstance),
                new SwordBossSMRun(bossInstance),
            }),
            new SwordBossFloat(bossInstance, transform, body),
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
            BunnyEventManager.Instance.Fire<bool>("OnSwordBossDied", new BunnyMessage<bool>(true, this));
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