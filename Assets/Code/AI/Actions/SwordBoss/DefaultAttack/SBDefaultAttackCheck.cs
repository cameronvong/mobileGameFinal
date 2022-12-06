using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class SBDefaultAttackCheck : BTNode
{
    SwordBossBT boss;
    public SBDefaultAttackCheck(SwordBossBT boss)
    {
        this.boss = boss;
    }

    public override BTNodeState Evaluate()
    {
        if (boss.DefaultAttackTimer >= boss.SwordBossData.AttackCooldown)
        {
            boss.body.velocity = Vector2.zero;
            state = BTNodeState.RUNNING;
            return state; 
        }
        state = BTNodeState.FAILURE;
        return state;
    }
}