using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeState : PlayerGroundedState
{

    public float DashCooldownTime;
    public float DashTime;
    public float DashSpeed;
    private bool CanDash = true;

    public PlayerEvadeState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
        DashCooldownTime = player.PlayerStats.DashCooldownTime;
        DashTime = player.PlayerStats.DashTime;
        DashSpeed = player.PlayerStats.DashSpeed;
        statePriority = 2;
    }

    public override void Enter() {
        if(!Validate()) return;
        stateMachine.SetPerformingAction(true);
        base.Enter();
        player.StartCoroutine(TriggerAnim());
        Execute();
        // player.StartCoroutine(Execute());
        // player.rigidBody2D.velocity = new Vector2(transform.localScale.x * DashSpeed, 0f);
        // yield return new WaitForSeconds(dashTime);
        // stateMachine.ChangeState(player.IdleState);
        // rb.gravityScale = originalGrav;
        // yield return new WaitForSeconds(dashCD);
    }

    public void Execute() {
        if(!Validate()) return;
        player.CurrentDashTime = 0f;
        int dashDir = !player.spriteRenderer.flipX ? 1 : -1;
        player.rigidBody2D.velocity = new Vector2(dashDir * DashSpeed, 0f);
    }

    private IEnumerator TriggerAnim() {
        // player.AnimComponent.SetBool(animationName, true);
        yield return new WaitForSeconds(DashTime);
        stateMachine.SetPerformingAction(false);
        stateMachine.ChangeState(stateMachine.GetPreviousState());
    }

    // private IEnumerator Execute() {
    //     Debug.Log($"Dash request start!");
    //     int dashDir = !player.spriteRenderer.flipX ? 1 : -1;
    //     player.rigidBody2D.velocity = new Vector2(dashDir * DashSpeed, 0f);
    //     yield return new WaitForSeconds(DashTime);
    //     stateMachine.ChangeState(stateMachine.GetPreviousState());
    //     CanDash = false;
    //     yield return new WaitForSeconds(DashCooldownTime);
    //     CanDash = true;
    // }

    public override bool Validate() {
        if (!base.Validate()) return false;
        return player.CurrentDashTime >= DashCooldownTime;
    }
}
