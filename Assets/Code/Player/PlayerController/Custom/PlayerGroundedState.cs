using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.PlayerControllerMachine.States;

public class PlayerGroundedState : PlayerState
{
    public override void Enter() {}
    public override void Exit() {}
    public override void HandleInput() {}
    public override void Update() {}
    public override void PhysicsUpdate() {}
    public override bool Validate() {
        return true;
    }

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
        
    }
}

