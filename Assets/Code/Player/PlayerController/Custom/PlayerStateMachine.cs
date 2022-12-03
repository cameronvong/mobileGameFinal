using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.EntityStateController.State;
using Jin.PlayerControllerMachine.States;

public class PlayerStateMachine : StateMachine<PlayerState>
{
    protected PlayerState FallbackState;
    protected PlayerState PreviousState;
    protected bool PerformingAction;
    
    public void Initialize(PlayerState state, PlayerState fallbackState = null)
    {
        currentState = state;
        currentState.Enter();
        this.FallbackState = fallbackState;
    }

    public override void ChangeState(PlayerState nextState)
    {
        if(PerformingAction && nextState.GetStatePriority() <= currentState.GetStatePriority()) return;
        // if(currentState != null && currentState.IsAction()) return;
        Debug.Log($"Changing state {nextState}");
        currentState?.Exit();
        PreviousState = currentState;
        if(!nextState.Validate()) {
            if(FallbackState != null) {
                currentState = FallbackState;
                currentState.Enter();
            }
            return;
        }
        currentState = nextState;
        currentState.Enter();
    }

    public virtual void ForceChangeState(PlayerState nextState) {
        base.ChangeState(nextState);
    }

    public virtual void SetPerformingAction(bool performingAction) {
        this.PerformingAction = performingAction;
    }

    public PlayerState GetPreviousState() {
        return PreviousState;
    }
}
