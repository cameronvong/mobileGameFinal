using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class SwordBossFloat : BTNode
{
    public float MaxHeight = 5.0f;

    public float currentHeight;
    public Transform bossTransform;
    public Rigidbody2D body;
    public SwordBossBT boss;

    bool changeDirection = false;
    public bool forceAdded = false;

    public SwordBossFloat(SwordBossBT boss, Transform transform, Rigidbody2D rigidBody)
    {
        this.boss= boss;
        body = rigidBody;
        bossTransform = transform;
        currentHeight = transform.position.y;
    }

    public override BTNodeState Evaluate()
    {
        Debug.Log("Adding force");
        currentHeight = bossTransform.position.y;
        if (currentHeight >= MaxHeight) {
            changeDirection = true;
            forceAdded = false;
        }

        if(currentHeight < 1f) {
            changeDirection = false;
            forceAdded = false;
        }

        if (!changeDirection) {
            body.velocity = bossTransform.up * boss.GeneralData.WalkingSpeed;
            // body.AddForce(bossTransform.up * boss.GeneralData.WalkingSpeed);
            forceAdded = true;
        } else if  (changeDirection) {
            body.velocity = bossTransform.up * -boss.GeneralData.WalkingSpeed;
            // body.AddForce(bossTransform.up * -boss.SwordBossData.WalkingSpeed);
            forceAdded = true;
        }
        state = BTNodeState.SUCCESS;
        return state;
    }
}
