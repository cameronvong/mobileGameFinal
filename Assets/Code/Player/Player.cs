using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.PlayerControllerMachine.States;

public class Player : MonoBehaviour
{
    public PlayerStateMachine stateMachine { get; private set; }
    
    // STATES
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    
    public Animator AnimComponent { get; private set; }

    public bool Grounded = true;
    // Start is called before the first frame update
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, stateMachine, "idle");
    }

    void Start()
    {
        // movementSM.ChangeState(movementSM.IdleState);
        AnimComponent = GetComponent<Animator>();
        stateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate() {
        stateMachine.PhysicsUpdate();
    }
}
