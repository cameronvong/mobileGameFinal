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
        var multiplier = boss.Enraged ? 0.2 : 1;
        if (boss.MeleeAttackTimer >= boss.GeneralData.AttackCooldown * multiplier) {
            state = BTNodeState.SUCCESS;
            return state;
        }
        state = BTNodeState.FAILURE;
        return state;
    }
}