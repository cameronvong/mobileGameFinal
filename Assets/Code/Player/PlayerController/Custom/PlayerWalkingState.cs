using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingState : PlayerMoveState
{
    public PlayerWalkingState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
        
    }

    protected override void Move()
    {
        if(!base.Validate() || movementInput == Vector2.zero)
            return;
        // player.spriteRenderer.flipX = movementInput.x < 0;
        SetHorizontalMovement(WalkingSpeedModifier * movementInput.x);
    }
}
