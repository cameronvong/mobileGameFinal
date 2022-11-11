using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.PlayerControllerMachine.States;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
        
    }
}