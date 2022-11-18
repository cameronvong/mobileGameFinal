using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.PlayerControllerMachine.States;

[RequireComponent(typeof(PlayerInputManager))]
public class Player : MonoBehaviour
{
    public PlayerInputManager InputManager { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }

    public Vector3 Position => transform.position;
    public Vector2 Velocity => rigidBody2D.velocity;

    public PlayerData PlayerStats;
    public float Stamina;

    [SerializeField] private LayerMask groundLayer;
    
    // STATES
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState WalkState { get; private set; }
    
    public Animator AnimComponent { get; private set; }
    public Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;

    private bool checkDash = true;
    private bool isDashing;
    private float dashSpeed = 10f;
    private float dashTime = 0.4f;
    private float dashCD = 1.2f;

    // Start is called before the first frame update
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, stateMachine, "idle");
        WalkState = new PlayerWalkingState(this, stateMachine, "walk");
        
        InputManager = GetComponent<PlayerInputManager>();
        AnimComponent = GetComponentInChildren<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        Stamina = PlayerStats.Stamina;
    }

    void Start()
    {
        // movementSM.ChangeState(movementSM.IdleState);
        stateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) {
            return;
        }
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate() {
        if (isDashing) {
            return;
        }
        stateMachine.PhysicsUpdate();
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }

    private IEnumerator Dash() {
        checkDash = false;
        isDashing = true;
        float originalGrav = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0f);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGrav;
        yield return new WaitForSeconds(dashCD);
        checkDash = true;
    }
}
