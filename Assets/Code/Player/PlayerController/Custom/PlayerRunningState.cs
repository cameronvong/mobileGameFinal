using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerMoveState
{
    public PlayerRunningState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {

    }

    public override void Enter() {
        if(!Validate()) return;
        base.Enter();
        player.StartCoroutine(HandleRun());
    }

    public override void Exit() {
        base.Exit();
        player.StartCoroutine(ReplenishStamina());
    }

    public override bool Validate() {
        if(base.Validate()) {
            return player.Stamina > 0;
        }
        return false;
    }

    private IEnumerator HandleRun()
    {
        while(Validate()) 
        { 
            Debug.Log ("OnCoroutine: "+(int)Time.time);
            player.Stamina -= 5;
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator ReplenishStamina()
    {
        while(stateMachine.currentState != this && player.Stamina < 100) 
        { 
            Debug.Log ("OnCoroutine: "+(int)Time.time);
            player.Stamina += 1;
            yield return new WaitForSeconds(1f);
        }
    }

    public override void Update() {
        if(!Validate())
            return;
        base.Update();
    }

    protected override void Move()
    {
        if(!Validate() || movementInput == Vector2.zero)
            return;
        // player.spriteRenderer.flipX = movementInput.x < 0;
        SetHorizontalMovement(RunningSpeedModifier * movementInput.x);
    }
}
