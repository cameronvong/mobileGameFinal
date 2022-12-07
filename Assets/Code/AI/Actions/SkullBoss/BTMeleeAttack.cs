using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;
using Bunny;
using Bunny.Tools;

public class BTMeleeAttack : BTNode
{
    BTTree boss;

    public BTMeleeAttack(BTTree boss)
    {
        this.boss = boss;
    }

    public IEnumerator AttackLengthEnd() {
        state = BTNodeState.RUNNING;
        yield return new WaitForSeconds(boss.GeneralData.MeleeAttackAnimLength);
        BunnyEventManager.Instance.Fire<float>("DamagePlayerRequest", new BunnyMessage<float>(10f, this));
        state = BTNodeState.SUCCESS;
    }

    public override BTNodeState Evaluate()
    {
        // boss.CollisionAttacking = true;
        boss.animator.SetTrigger("meleeAttack");
        boss.StartCoroutine(AttackLengthEnd());
        boss.MeleeAttackTimer = 0f;
        return state;
    }
}