using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.PlayerControllerMachine.States;

public class PlayerMoveState : PlayerGroundedState
{
    protected Vector2 WorkspaceMovementVector;
    protected float WalkingSpeedModifier = 5f;
    protected float RunningSpeedModifier = 10f;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
        WalkingSpeedModifier = player.PlayerStats.WalkingSpeed;
        RunningSpeedModifier = player.PlayerStats.RunningSpeed;
        // statePriority = 999;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(movementInput.x == 0f)
        {
            stateMachine.ChangeState(player.IdleState);
        } else {
            Jump();
            Move();
        }
    }

    protected virtual void Jump()
    {
        Debug.Log($"Y input is {movementInput.y}, {player.IsGrounded()}");
        if (player.IsGrounded() && movementInput.y >= 0.5)
        {
            Debug.Log("Jumping");
            player.rigidBody2D.velocity += new Vector2(0, player.PlayerStats.JumpForce * 0.2f);
        }
    }

    protected virtual void Move()
    {
        if(!base.Validate() || movementInput == Vector2.zero)
            return;
        Debug.Log($"Move vector {WalkingSpeedModifier * movementInput.x}");
        SetHorizontalMovement(WalkingSpeedModifier * movementInput.x);
    }

    protected virtual void SetHorizontalMovement(float velocityX)
    {
        WorkspaceMovementVector.Set(velocityX, player.Velocity.y);
        player.spriteRenderer.flipX = movementInput.normalized.x < 0;
        player.rigidBody2D.velocity = WorkspaceMovementVector;
    }
}