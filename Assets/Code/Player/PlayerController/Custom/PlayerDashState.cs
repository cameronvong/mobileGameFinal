using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeState : PlayerGroundedState
{

    public float DashCooldownTime;
    public float DashTime;
    public float DashSpeed;

    public PlayerEvadeState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
        DashCooldownTime = player.PlayerStats.DashCooldownTime;
        DashTime = player.PlayerStats.DashTime;
        DashSpeed = player.PlayerStats.DashSpeed;
    }

    public override void Enter() {
        if(!Validate()) return;
        base.Enter();
        // player.rigidBody2D.velocity = new Vector2(transform.localScale.x * DashSpeed, 0f);
        // yield return new WaitForSeconds(dashTime);
        // stateMachine.ChangeState(player.IdleState);
        // rb.gravityScale = originalGrav;
        // yield return new WaitForSeconds(dashCD);
    }

    public override bool Validate() {
        if (!base.Validate()) return false;
        return player.CurrentDashTime >= DashCooldownTime;
    }
}
