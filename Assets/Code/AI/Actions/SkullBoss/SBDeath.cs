using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class SBDeath : BTNode
{
    public BTTree boss;
    public bool animatorSet;

    public SBDeath(BTTree boss)
    {
        this.boss = boss;
    }

    public override BTNodeState Evaluate()
    {
        if (boss.Health <= 0) {
            if(!animatorSet) {
                Debug.Log("Triggering death anim");
                boss.animator.SetBool("death", true);
                animatorSet = true;
            }
            state = BTNodeState.SUCCESS;
            return state;
        }
        animatorSet = false;
        state = BTNodeState.FAILURE;
        return state;
    }
}
