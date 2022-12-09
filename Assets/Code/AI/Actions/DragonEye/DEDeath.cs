using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class DEDeath : BTNode
{
    DragonEyeBT boss;
    bool animatorSet;
    public DEDeath(DragonEyeBT boss)
    {
        this.boss = boss;
    }

    public override BTNodeState Evaluate()
    {
        if (boss.Health <= 0)
        {
            if(!animatorSet)
                boss.animator.SetBool("death", true);
            animatorSet = true;
            boss.body.sharedMaterial = new PhysicsMaterial2D();
            boss.body.AddForce(boss.transform.up * -10f);
            state = BTNodeState.SUCCESS;
            return state;
        }
        animatorSet = false;
        boss.animator.SetBool("death", false);
        state = BTNodeState.FAILURE;
        return state;
    }
}