using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.PlayerControllerMachine.States;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Player is idle");
    }

    public override void PhysicsUpdate() { 
        base.PhysicsUpdate();
        Debug.Log(movementInput);
        if(movementInput.x != 0f)
        {
            stateMachine.ChangeState(player.WalkState);
        }
    }
}