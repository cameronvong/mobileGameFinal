using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jin.EntityStateController.State
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
        void PhysicsUpdate();
        void HandleInput();
    }
}