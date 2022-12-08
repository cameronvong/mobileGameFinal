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

    public IEnumerator StayInGround() {
        state = BTNodeState.SUCCESS;
        yield return new WaitForSeconds(3);
        boss.MeleeAttackTimer = 0f;
        state = BTNodeState.FAILURE;
        parent.parent.DeleteData("plunged");
        parent.parent.DeleteData("target");
    }

    public override BTNodeState Evaluate()
    {
        object t = parent.parent.GetData("plunged");
        Debug.Log("Adding force down");
        if(boss.transform.position.y >= 0f) {
            boss.CollisionAttacking = true;
            boss.body.AddForce(boss.transform.up * -40f);
        } else {
            // parent.parent.DeleteData("target");
            if (t == null) {
                parent.parent.SetData("plunged", true);
                BunnyEventManager.Instance.Fire<CameraEffectRequestPayload>("CameraEffectRequest", new BunnyMessage<CameraEffectRequestPayload>() {
                    payload = new CameraEffectRequestPayload() {
                        duration = 0.5f,
                        effectType = CameraEffectType.SHAKE,
                    },
                    sender = boss
                });
                boss.StartCoroutine(StayInGround());
            }
            boss.CollisionAttacking = false;
            boss.body.velocity = Vector2.zero;;
        }
        state = BTNodeState.SUCCESS;
        return state;
    }
}
