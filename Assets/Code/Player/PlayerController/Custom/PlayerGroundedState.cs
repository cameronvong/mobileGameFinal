using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.PlayerControllerMachine.States;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 movementInput;

    public override void Enter() {
        if(!Validate())
            return;
        base.Enter();
    }
    
    public override void Exit() {
        base.Exit();
     }

    public override void HandleInput() {
        if(!Validate())
            return;
        ReadMovementInput();
    }

    public override void Update() {
        if(!Validate())
            return;
    }

    public override void PhysicsUpdate() { 
        if(!Validate())
            return;
    }

    public override bool Validate() {
        return player.IsGrounded();
    }

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animationName = animationName;
    }

    protected virtual void ReadMovementInput()
    {
        movementInput = player.InputManager.playerControls.Gameplay.Movement.ReadValue<Vector2>();
        Vector2 movementTD = player.InputManager.playerControls.Gameplay.Movement.ReadValue<Vector2>();
    }

    
}

