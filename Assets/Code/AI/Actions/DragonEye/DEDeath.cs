using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class DEDeath : BTNode
{
    DragonEyeBT boss;
    public DEDeath(DragonEyeBT boss)
    {
        this.boss = boss;
    }

    public override BTNodeState Evaluate()
    {
        if (boss.Health <= 0)
        {
            boss.physicsCollider.isTrigger = true;
            boss.body.AddForce(boss.transform.up * -10f);
            state = BTNodeState.SUCCESS;
            return state;
        }
        state = BTNodeState.FAILURE;
        return state;
    }
}