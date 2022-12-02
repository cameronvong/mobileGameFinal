using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class SwordBossSummonMinion : BTNode
{
    SwordBossBT boss;
    public SwordBossSummonMinion(SwordBossBT boss)
    {
        this.boss = boss;
    }

    public override BTNodeState Evaluate()
    {
        if (boss.SpecialTimer >= boss.SwordBossData.SpecialAttackCooldown)
        {
            boss.body.velocity = Vector2.zero;
            state = BTNodeState.SUCCESS;
            return state; 
        }
        state = BTNodeState.FAILURE;
        return state;
    }
}
