using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jin.EntityStateController.State
{
    public abstract class StateMachine
    {
        public IState currentState { get; protected set; }

        public virtual void ChangeState(IState nextState)
        {
            currentState?.Exit();
            currentState = nextState;
            currentState.Enter();
        }

        public virtual void HandleInput()
        {
            currentState?.HandleInput();
        }

        public virtual void Update()
        {
            currentState?.Update();
        }

        public virtual void PhysicsUpdate()
        {
            currentState?.PhysicsUpdate();
        }
    }
}