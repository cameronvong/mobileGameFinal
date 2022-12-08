using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class SwordBossMoveTowardsPlayer : BTNode
{
    SwordBossBT boss;
    public SwordBossMoveTowardsPlayer(SwordBossBT boss)
    {
        this.boss = boss;
    }

    public override BTNodeState Evaluate()
    {
        object t = GetData("target");
        // if (boss.MeleeAttackTimer < boss.GeneralData.AttackCooldown)
        // {
        //     state = BTNodeState.FAILURE;
        //     return state;
        // }
        Debug.Log($"Target is {parent.parent} {parent.parent.GetData("target") != null}");
        if (t == null)
        {
            Debug.Log("Moving");
            // boss.DefaultAttackTimer = 0f;
            Vector3 dir = boss.target.transform.position - boss.transform.position;
            dir.y = 8f;
            if (boss.transform.position.y >= 8f) {
                dir.y = 0;
            }
            // dir.y = 0;
            dir.Normalize();
            boss.body.velocity = dir * boss.GeneralData.RunningSpeed;
            RaycastHit2D hit = Physics2D.Raycast(boss.transform.position, Vector2.down, Mathf.Infinity, boss.playerMask.value);
            Debug.DrawRay(boss.transform.position, Vector2.down * 1000f, Color.green);
            if(hit.collider != null && boss.transform.position.y >= 8f)
            {
                Debug.Log($"did hit {hit}");
                parent.parent.SetData("target", boss.target.transform);
                state = BTNodeState.SUCCESS;
                return state; 
            }
            // // boss.body.velocity = Vector2.zero;
            // state = BTNodeState.FAILURE;
            // return state; 
        } else {
            state = BTNodeState.FAILURE;
            return state; 
        }
        state = BTNodeState.SUCCESS;
        return state; 
    }
}
