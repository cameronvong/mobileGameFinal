using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class SwordBossGroundStuck : BTNode
{
    SwordBossBT boss;
    public SwordBossGroundStuck(SwordBossBT boss)
    {
        this.boss = boss;
    }

    public IEnumerator StayInGround() {
        state = BTNodeState.RUNNING;
        yield return new WaitForSeconds(3);
        boss.DefaultAttackTimer = 0f;
        state = BTNodeState.SUCCESS;
    }

    public override BTNodeState Evaluate()
    {
        if (boss.DefaultAttackTimer < boss.SwordBossData.AttackCooldown)
        {
            state = BTNodeState.FAILURE;
            return state;
        }
        boss.StartCoroutine(StayInGround());
        return state;
    }
}