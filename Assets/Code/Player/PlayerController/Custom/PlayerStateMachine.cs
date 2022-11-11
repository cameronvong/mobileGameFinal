using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.EntityStateController.State;
using Jin.PlayerControllerMachine.States;

public class PlayerStateMachine : StateMachine
{
    public void Initialize(PlayerState state)
    {
        currentState = state;
        currentState.Enter();
    }
}
