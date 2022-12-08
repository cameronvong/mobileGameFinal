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
        if (boss.MeleeAttackTimer >= boss.GeneralData.AttackCooldown)
        {
            Debug.Log("Attack is ready");
            // boss.body.velocity = Vector2.zero;
            state = BTNodeState.SUCCESS;
            return state; 
        }
        state = BTNodeState.FAILURE;
        return state;
    }
}