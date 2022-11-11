using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.EntityStateController.State; 

namespace Jin.PlayerControllerMachine.States
{
   public class PlayerMovementStateMachine : StateMachine
   {
        // public PlayerIdleState IdleState { get; }
        public PlayerWalkingState WalkingState { get; }
        public PlayerRunningState RunningState { get; }

        public PlayerMovementStateMachine()
        {
            // IdleState = new PlayerIdleState();
            WalkingState = new PlayerWalkingState();
            RunningState = new PlayerRunningState();
        }
   } 
}