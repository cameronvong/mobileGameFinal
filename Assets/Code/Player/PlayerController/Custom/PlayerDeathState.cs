using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.PlayerControllerMachine.States;

public class PlayerDeathState : PlayerState
{
    public override void Enter() {
        if(!Validate())
            return;
        Debug.Log("Entering death state");
        stateMachine.SetPerformingAction(true);
        player.AnimComponent.SetTrigger(animationName);
        // base.Enter();
    }
    
    public override void Exit() {
        Debug.Log("Exiting death state");
        stateMachine.SetPerformingAction(false);
        base.Exit();
     }

    public override void HandleInput() {
        if(!Validate())
            return;
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
        return player.Health <= 0;
    }

    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
        statePriority = 9999;
    }

}
