using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class BTCheckMeleeRange : BTNode
{
    BTTree boss;

    public BTCheckMeleeRange(BTTree boss)
    {
        this.boss = boss;
    }

    public override BTNodeState Evaluate()
    {
        Vector3 dir = boss.target.transform.position - boss.transform.position;
        // dir.y = 0;
        if (Mathf.Abs(dir.x) <= boss.GeneralData.MeleeAttackRange) {
            state = BTNodeState.SUCCESS;
            return state;
        }
        state = BTNodeState.FAILURE;
        return state;
    }
}