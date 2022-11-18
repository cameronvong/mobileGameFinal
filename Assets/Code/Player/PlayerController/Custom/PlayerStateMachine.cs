using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.EntityStateController.State;
using Jin.PlayerControllerMachine.States;

public class PlayerStateMachine : StateMachine<PlayerState>
{
    public void Initialize(PlayerState state)
    {
        currentState = state;
        currentState.Enter();
    }

    public override void ChangeState(PlayerState nextState)
    {
        if(!nextState.Validate()) return;
        currentState?.Exit();
        currentState = nextState;
        currentState.Enter();
    }
}
