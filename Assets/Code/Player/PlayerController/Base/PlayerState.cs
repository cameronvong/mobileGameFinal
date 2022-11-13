using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.EntityStateController.State;

namespace Jin.PlayerControllerMachine.States
{
    public abstract class PlayerState : IState
    {
        protected Player player;
        protected PlayerStateMachine stateMachine;

        protected string animationName;

        public virtual void Enter()
        {
            if(!Validate())
                return;
            player.AnimComponent.SetBool(animationName, true);
        }

        public virtual void Exit()
        {
           player.AnimComponent.SetBool(animationName, false); 
        }
        
        public abstract void HandleInput();
        public abstract void Update();
        public abstract void PhysicsUpdate();
        public abstract bool Validate();

        public PlayerState(Player player, PlayerStateMachine stateMachine, string animationName)
        {
            this.player = player;
            this.stateMachine = stateMachine;
            this.animationName = animationName;
        }
    }

}