using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerMoveState
{
    private float AttackCooldownTime;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, string animationName) : base(player, stateMachine, animationName)
    {
        AttackCooldownTime = player.PlayerStats.AttackSpeed;
        statePriority = 10;
    }

    protected override void Move()
    {
        if(!Validate() || movementInput == Vector2.zero)
            return;
        // player.spriteRenderer.flipX = movementInput.x < 0;
        SetHorizontalMovement(WalkingSpeedModifier * movementInput.x);
    }

    private IEnumerator HandleAnim() {
        yield return new WaitForSeconds(1f);
        stateMachine.SetPerformingAction(false);
        stateMachine.ChangeState(stateMachine.GetPreviousState());
    }

    public override void Enter() {
        if(!Validate()) return;
        Debug.Log("Attacking now");
        stateMachine.SetPerformingAction(true);
        base.Enter();
        player.StartCoroutine(HandleAnim());
        player.CurrentAttackTime = 0;
        // stateMachine.SetPerformingAction(false);
    }


    public override bool Validate() {
        if (!base.Validate()) return false;
        return player.CurrentAttackTime >= 1f/AttackCooldownTime;
    }
}
