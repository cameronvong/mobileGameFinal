using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.EntityStateController.State;
using Jin.PlayerControllerMachine.States;

public class PlayerStateMachine : StateMachine<PlayerState>
{
    protected PlayerState FallbackState;
    public void Initialize(PlayerState state, PlayerState fallbackState = null)
    {
        currentState = state;
        currentState.Enter();
        this.FallbackState = fallbackState;
    }

    public override void ChangeState(PlayerState nextState)
    {
        if(!nextState.Validate()) {
            if(FallbackState != null) {
                currentState?.Exit();
                currentState = FallbackState;
                currentState.Enter();
            }
            return;
        }
        currentState?.Exit();
        currentState = nextState;
        currentState.Enter();
    }
}
