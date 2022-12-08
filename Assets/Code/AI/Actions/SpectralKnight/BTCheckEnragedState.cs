using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class BTCheckEnragedState : BTNode
{
    BTTree boss;

    public BTCheckEnragedState(BTTree boss)
    {
        this.boss = boss;
    }

    public override BTNodeState Evaluate()
    {
        if (boss.Health <= boss.GeneralData.Health * boss.GeneralData.EnragePoint) {
            state = BTNodeState.SUCCESS;
            return state;
        }
        state = BTNodeState.FAILURE;
        return state;
    }
}