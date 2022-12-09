using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class SwordBossDeath : BTNode
{
    SwordBossBT boss;
    bool animatorSet;
    public SwordBossDeath(SwordBossBT boss)
    {
        this.boss = boss;
    }

    public override BTNodeState Evaluate()
    {
        if (boss.Health <= 0)
        {
            if(!animatorSet) {
                boss.mainCollider.isTrigger = false;
                boss.animator.SetBool("death", true);
                animatorSet = true;
            }
            boss.body.AddForce(boss.transform.up * -10f);
            state = BTNodeState.SUCCESS;
            return state;
        }
        boss.animator.SetBool("death", false);
        animatorSet = false;
        state = BTNodeState.FAILURE;
        return state;
    }
}