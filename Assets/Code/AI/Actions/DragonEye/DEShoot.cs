using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class DEShoot : BTNode
{
    DragonEyeBT boss;
    Vector2 currentVelocity;
    bool currentlyRunning = false;
    public DEShoot(DragonEyeBT boss)
    {
        this.boss = boss;
    }

    private IEnumerator ShootProjectiles()
    {
        float speed = boss.Enraged ? 20f : 10f;
        for (var i = 0; i <= 5; i++) {
            boss.DefaultAttackTimer = 0f;
            GameObject projectile = (GameObject) Object.Instantiate(boss.DEProjectile, boss.transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = (boss.target.transform.position - projectile.transform.position).normalized * speed;
            yield return new WaitForSeconds(0.5f);
        }
        currentlyRunning = false;
        state = BTNodeState.SUCCESS;
        boss.body.velocity = currentVelocity;
    }

    public override BTNodeState Evaluate()
    {
        if (currentlyRunning) {
            state = BTNodeState.SUCCESS;
            return state;
        }
        if (boss.DefaultAttackTimer >= boss.BossData.AttackCooldown)
        {
            boss.DefaultAttackTimer = 0f;
            currentVelocity = boss.body.velocity;
            boss.body.velocity = Vector2.zero;
            currentlyRunning = true;
            boss.StartCoroutine(ShootProjectiles());
            state = BTNodeState.SUCCESS;
            return state;
        }
        state = BTNodeState.FAILURE;
        return state;
    }
}