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
        boss.MeleeAttackTimer = 0f;
        state = BTNodeState.SUCCESS;
        parent.parent.DeleteData("plunged");
        parent.parent.DeleteData("target");
    }

    public override BTNodeState Evaluate()
    {
        if (boss.MeleeAttackTimer < boss.GeneralData.AttackCooldown)
        {
            state = BTNodeState.FAILURE;
            return state;
        }
        boss.StartCoroutine(StayInGround());
        return state;
    }
}