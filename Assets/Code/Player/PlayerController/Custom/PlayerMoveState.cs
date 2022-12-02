using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.PlayerControllerMachine.States;

public class PlayerMoveState : PlayerGroundedState
{
    protected Vector2 WorkspaceMovementVector;
    protected float WalkingSpeedModifier = 5f;
    protected float RunningSpeedModifier = 10f;
    private float FacingDirection = 1f;

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
            // Debug.Log($"{movementInput.normalized.x}");
            Move();
        }
    }

    protected virtual void Move()
    {
        if(!base.Validate() || movementInput == Vector2.zero)
            return;
        // player.spriteRenderer.flipX = movementInput.x < 0;
        Debug.Log($"Move vecotr {WalkingSpeedModifier * movementInput.x}");
        SetHorizontalMovement(WalkingSpeedModifier * movementInput.x);
    }

    protected virtual void SetHorizontalMovement(float velocityX)
    {
        // Debug.Log("Velocity should be ");
        WorkspaceMovementVector.Set(velocityX, player.Velocity.y);
        ShouldFlip(movementInput.normalized.x);
        // Debug.Log($"New force {WorkspaceMovementVector}");
        // player.rigidBody2D.AddForce(WorkspaceMovementVector * 100, ForceMode2D.Force);
        player.rigidBody2D.velocity = WorkspaceMovementVector;
    }

    private void ShouldFlip(float xInput)
    {
        if(xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingDirection *= -1;
        Debug.Log($"Flipped! {FacingDirection}");
        player.spriteRenderer.flipX = FacingDirection == -1;
        // player.rigidBody2D.velocity -= player.rigidBody2D.velocity;
        // var opposite = -player.rigidBody2D.velocity;
    }
}