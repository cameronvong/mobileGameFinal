using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using AI.BehaviourTree;

public class SwordBossSMRun : BTNode
{
    SwordBossBT boss;
    bool specialAttacking = false;
    public SwordBossSMRun(SwordBossBT boss)
    {
        this.boss = boss;
    }

    private void SpawnSword()
    {
        var randomX = Random.Range(boss.SpawnAreaCollider.bounds.min.x, boss.SpawnAreaCollider.bounds.max.x);
        var sword = Object.Instantiate(boss.FallingSwordPrefab, new Vector3(randomX, boss.SpawnAreaCollider.bounds.min.y), Quaternion.identity);
        // sword.rigidBody2DSetForce(Vector2.zero);
    }

    private IEnumerator SpawnSpecialAttack()
    {
        specialAttacking = true;
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds((float) Random.Range(0.5f, 1f));
            SpawnSword();
        }
        boss.SpecialTimer = 0f;
        specialAttacking = false;
    }

    public override BTNodeState Evaluate()
    {
        // Debug.Log($"Attempt to eval {t != null} {parent}");

        Debug.Log("Spawning swords");
        if (!specialAttacking) {
            boss.StartCoroutine(SpawnSpecialAttack());
        }

        state = BTNodeState.RUNNING;
        return state;
    }
}
