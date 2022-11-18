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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(movementInput.x == 0f)
        {
            // stateMachine.ChangeState(player.IdleState);
        } else {
            Move();
        }
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
