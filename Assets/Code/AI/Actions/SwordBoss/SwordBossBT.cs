using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviourTree;

public class SwordBossBT: BTTree
{
    public AIData SwordBossData;
    public float SpecialTimer;

    // Special 1
    public Collider2D SpawnAreaCollider;
    public GameObject FallingSwordPrefab;
    public GameObject SACGO;

    protected override BTNode SetupTree()
    {
        SwordBossBT bossInstance = this;
        SpecialTimer = 0f;
        SpawnAreaCollider = SACGO.GetComponent<BoxCollider2D>();

        BTNode root = new BTSelector(new List<BTNode>
        {
            new BTSequence(new List<BTNode>
            {
                new SwordBossSummonMinion(bossInstance),
                new SwordBossSMRun(bossInstance),
            }),
            new SwordBossFloat(transform, body),
        });
        return root;
    }

    protected override void OnUpdate()
    {
        SpecialTimer += Time.deltaTime;
    }
}