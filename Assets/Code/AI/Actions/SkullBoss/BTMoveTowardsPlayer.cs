using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class BTMoveTowardsPlayer : BTNode
{
    BTTree boss;

    public BTMoveTowardsPlayer(BTTree boss)
    {
        this.boss = boss;
    }

    public override BTNodeState Evaluate()
    {
        Vector3 dir = boss.target.transform.position - boss.transform.position;
        // dir.y = 0;
        dir.Normalize();
        boss.body.velocity = dir * boss.GeneralData.WalkingSpeed;
        // boss.body.MovePosition(boss.transform.position + (dir * boss.GeneralData.WalkingSpeed * Time.deltaTime));
        state = BTNodeState.SUCCESS;
        return state;
    }
}