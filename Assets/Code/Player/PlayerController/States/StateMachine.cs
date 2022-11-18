using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jin.EntityStateController.State
{
    public abstract class StateMachine<T> where T : IState
    {
        public T currentState { get; protected set; }

        public virtual void ChangeState(T nextState)
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