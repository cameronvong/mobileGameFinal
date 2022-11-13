using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.PlayerControllerMachine.States;

public class PlayerMoveState : PlayerGroundedState
{
    protected Vector2 WorkspaceMovementVector;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
        
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
        Debug.Log("Player is moving");
        SetHorizontalMovement(5f * movementInput.x);
    }

    protected virtual void SetHorizontalMovement(float velocityX)
    {
        WorkspaceMovementVector.Set(velocityX, player.Velocity.y);
        player.rigidBody2D.velocity = WorkspaceMovementVector;
    }
}