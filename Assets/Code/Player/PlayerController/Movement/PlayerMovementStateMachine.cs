using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.Controller.State; 
using Jin.Player.States;

namespace Jin.Player.States
{
   public class PlayerMovementStateMachine : StateMachine
   {
        public PlayerIdleState IdleState { get; }
        public PlayerWalkingState WalkingState { get; }
        public PlayerRunningState RunningState { get; }

        public PlayerMovementStateMachine()
        {
            IdleState = new PlayerIdleState();
            WalkingState = new PlayerWalkingState();
            RunningState = new PlayerRunningState();
        }
   } 
}