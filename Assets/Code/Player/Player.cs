using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.Player.States;

public class Player : MonoBehaviour
{
    private PlayerMovementStateMachine movementSM;
    // Start is called before the first frame update
    private void Awake()
    {
        movementSM = new PlayerMovementStateMachine();
    }

    void Start()
    {
        movementSM.ChangeState(movementSM.IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        movementSM.HandleInput();
        movementSM.Update();
    }

    private void FixedUpdate() {
        movementSM.PhysicsUpdate();
    }
}
