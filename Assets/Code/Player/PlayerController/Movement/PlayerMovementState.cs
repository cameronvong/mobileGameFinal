using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.EntityStateController.State;

namespace Jin.PlayerControllerMachine.States
{
    public abstract class PlayerMovementState : IState
    {
        public abstract void Enter();
        public abstract void Exit();
        public abstract void HandleInput();
        public abstract void Update();
        public abstract void PhysicsUpdate();
    }
}