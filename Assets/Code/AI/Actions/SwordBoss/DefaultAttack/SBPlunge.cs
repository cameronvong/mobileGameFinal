using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;
using Bunny;
using Bunny.Tools;

public class SBPlunge : BTNode
{
    SwordBossBT boss;
    public SBPlunge(SwordBossBT boss)
    {
        this.boss = boss;
    }

    public override BTNodeState Evaluate()
    {
        object t = parent.parent.GetData("plunged");
        if (boss.DefaultAttackTimer < boss.SwordBossData.AttackCooldown)
        {
            state = BTNodeState.FAILURE;
            return state;
        }
        if(t != null) {
            state = BTNodeState.RUNNING;
            return state;
        }
        Debug.Log("Adding force down");
        if(boss.transform.position.y >= 0f) {
            boss.CollisionAttacking = true;
            boss.body.AddForce(boss.transform.up * -20f, ForceMode2D.Impulse);
        } else {
            // parent.parent.DeleteData("target");
            parent.parent.SetData("plunged", true);
            boss.CollisionAttacking = false;
            BunnyEventManager.Instance.Fire<CameraEffectRequestPayload>("CameraEffectRequest", new BunnyMessage<CameraEffectRequestPayload>() {
                payload = new CameraEffectRequestPayload() {
                    duration = 0.5f,
                    effectType = CameraEffectType.SHAKE,
                },
                sender = boss
            });
            boss.body.velocity = Vector2.zero;
        }
        state = BTNodeState.SUCCESS;
        return state;
    }
}
