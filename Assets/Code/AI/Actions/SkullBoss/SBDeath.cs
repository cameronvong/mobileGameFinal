using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class SBDeath : BTNode
{
    public BTTree boss;

    public SBDeath(BTTree boss)
    {
        this.boss = boss;
    }

    public override BTNodeState Evaluate()
    {
        if (boss.Health <= 0) {
            //boss.transform.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = true;
            boss.body.AddForce(Vector2.down * 10f);
            state = BTNodeState.SUCCESS;
            return state;
        }
        state = BTNodeState.FAILURE;
        return state;
    }
}
