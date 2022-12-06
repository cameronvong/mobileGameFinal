using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.PlayerControllerMachine.States;

public class PlayerHurtState : PlayerState
{

    public override void Enter() {
        if(!Validate())
            return;
        base.Enter();
        stateMachine.SetPerformingAction(true);
        // player.AnimComponent.SetTrigger(animationName);
        player.StartCoroutine(HandleHurt());
    }
    
    public override void Exit() {
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

    public IEnumerator HandleHurt()
    {
        yield return new WaitForSeconds(0.5f);
        stateMachine.SetPerformingAction(false);
        player.AnimComponent.SetBool(animationName, false);
        stateMachine.Fallback();
    }

    public override bool Validate() {
        // return player.IsGrounded();
        return true;
    }

    public PlayerHurtState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animationName = animationName;
        statePriority = 900;
    }    
}

