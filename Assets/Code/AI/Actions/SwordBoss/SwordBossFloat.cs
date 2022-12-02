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

    public SwordBossFloat(Transform transform, Rigidbody2D rigidBody)
    {
        body = rigidBody;
        bossTransform = transform;
        currentHeight = transform.position.y;
    }

    public override BTNodeState Evaluate()
    {
        Debug.Log("Adding force");
        currentHeight = bossTransform.position.y;
        if (currentHeight <= MaxHeight) {
            body.AddForce(bossTransform.up * 1.0f);
        } else {
            body.AddForce(bossTransform.up * -1.0f);
        }
        state = BTNodeState.RUNNING;
        return state;
    }
}
