using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class BTCheckMeleeAttackTimer : BTNode
{
    BTTree boss;

    public BTCheckMeleeAttackTimer(BTTree boss)
    {
        this.boss = boss;
    }

    public override BTNodeState Evaluate()
    {
        if (boss.MeleeAttackTimer >= boss.GeneralData.AttackCooldown) {
            state = BTNodeState.SUCCESS;
            return state;
        }
        state = BTNodeState.FAILURE;
        return state;
    }
}