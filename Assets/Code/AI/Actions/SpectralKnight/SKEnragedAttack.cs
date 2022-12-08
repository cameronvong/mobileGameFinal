using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;
using Bunny;
using Bunny.Tools;

public class SKEnragedAttack : BTNode
{
    BTTree boss;

    public SKEnragedAttack(BTTree boss)
    {
        this.boss = boss;
    }

    public IEnumerator AttackLengthEnd() {
        state = BTNodeState.RUNNING;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 3; i++) {
            Vector3 dir = boss.target.transform.position - boss.transform.position;
            if (Mathf.Abs(dir.x) <= boss.GeneralData.MeleeAttackRange) {
                BunnyEventManager.Instance.Fire<float>("DamagePlayerRequest", new BunnyMessage<float>(20f, this));
            }
            yield return new WaitForSeconds(0.5f);
        }
        state = BTNodeState.SUCCESS;
    }

    public override BTNodeState Evaluate()
    {
        // boss.CollisionAttacking = true;
        boss.animator.SetTrigger("enragedAttack");
        boss.StartCoroutine(AttackLengthEnd());
        boss.MeleeAttackTimer = 0f;
        return state;
    }
}