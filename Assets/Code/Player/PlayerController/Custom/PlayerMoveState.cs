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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(movementInput.x == 0f)
        {
            stateMachine.ChangeState(player.IdleState);
        } else {
            Move();
        }
    }

    protected virtual void Move()
    {
        if(!base.Validate() || movementInput == Vector2.zero)
            return;
        player.spriteRenderer.flipX = movementInput.x < 0;
        SetHorizontalMovement(WalkingSpeedModifier * movementInput.x);
    }

    protected virtual void SetHorizontalMovement(float velocityX)
    {
        WorkspaceMovementVector.Set(velocityX, player.Velocity.y);
        player.rigidBody2D.velocity = WorkspaceMovementVector;
    }
}