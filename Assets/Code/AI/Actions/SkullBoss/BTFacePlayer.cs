using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class BTFacePlayer : BTNode
{
    BTTree boss;
    private float baseScaleX;

    public BTFacePlayer(BTTree boss)
    {
        this.boss = boss;
        baseScaleX = boss.transform.localScale.x;
    }

    public override BTNodeState Evaluate()
    {
        var scale = boss.transform.localScale;
        scale.x = boss.transform.position.x > boss.target.transform.position.x ? -baseScaleX : baseScaleX;
        boss.transform.localScale = scale;
        state = BTNodeState.SUCCESS;
        return state;
    }
}
