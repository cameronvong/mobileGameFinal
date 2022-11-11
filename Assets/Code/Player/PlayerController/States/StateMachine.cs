using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jin.Controller.State
{
    public abstract class StateMachine
    {
        protected IState currentState;

        public void ChangeState(IState nextState)
        {
            currentState?.Exit();
            currentState = nextState;
            currentState.Enter();
        }

        public void HandleInput()
        {
            currentState?.HandleInput();
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void PhysicsUpdate()
        {
            currentState?.PhysicsUpdate();
        }
    }
}