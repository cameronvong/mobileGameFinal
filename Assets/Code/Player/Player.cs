using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jin.PlayerControllerMachine.States;
using Bunny.Tools;

[RequireComponent(typeof(PlayerInputManager))]
public class Player : MonoBehaviour
{
    public PlayerInputManager InputManager { get; private set; }
    public PlayerStateMachine stateMachine { get; private set; }

    public Vector3 Position => transform.position;
    public Vector2 Velocity => rigidBody2D.velocity;

    public PlayerData PlayerStats;

    public float Stamina;

    public float Health;
    public float CurrentAttackTime;

    [SerializeField] private LayerMask groundLayer;
    
    // STATES
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState WalkState { get; private set; }
    public PlayerEvadeState DodgeState { get; private set; }
    public PlayerAttackState AttackState {  get; private set; }
    
    public Animator AnimComponent { get; private set; }
    public Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;

    public float CurrentDashTime;
    public float GeneralLocalTime = 1f;

    // Events & Callbacks
    Action<BunnyMessage<float>> OnPlayerAttacked;


    // Start is called before the first frame update
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, stateMachine, "idle");
        WalkState = new PlayerWalkingState(this, stateMachine, "walk");
        DodgeState = new PlayerEvadeState(this, stateMachine, "dodge");
        AttackState = new PlayerAttackState(this, stateMachine, "attack");
        
        InputManager = GetComponent<PlayerInputManager>();
        AnimComponent = GetComponentInChildren<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        Stamina = PlayerStats.Stamina;
        Health = PlayerStats.MaxHealth;
        CurrentDashTime = PlayerStats.DashCooldownTime;
        CurrentAttackTime = 1f/PlayerStats.AttackSpeed;

        BunnyEventManager.Instance.RegisterEvent("DamagePlayerRequest", this);
    }

    void Start()
    {
        // movementSM.ChangeState(movementSM.IdleState);
        stateMachine.Initialize(IdleState, IdleState);

        OnPlayerAttacked = PlayerAttack;
        BunnyEventManager.Instance.OnEventRaised<float>("DamagePlayerRequest", OnPlayerAttacked);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
        CurrentDashTime += Time.deltaTime;
        Debug.Log($"Current state: {stateMachine.currentState}");
    }

    private void FixedUpdate() 
    {
        CurrentDashTime += Time.deltaTime;
        CurrentAttackTime += Time.deltaTime;
        GeneralLocalTime += Time.deltaTime;

        if(GeneralLocalTime >= 1f)
        {
            if(Stamina < 100) 
            {
                Stamina += 5;
            }
        }
        stateMachine.PhysicsUpdate();
    }

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }

    public void Dash() {
        stateMachine.ChangeState(DodgeState);
    }

    public void Attack() {
        Debug.Log("Attacking");
        stateMachine.ChangeState(AttackState);
        GameObject obj = GameObject.FindWithTag("Boss");
        // Debug.Log($"Distance is: {Vector3.Distance(obj.transform.position, transform.position)}");
        if (Vector3.Distance(obj.transform.position, transform.position) <= PlayerStats.MeleeAttackRange)
        {
           BunnyEventManager.Instance.Fire<float>("DamageBossRequest", new BunnyMessage<float>(10f, this));
        }
    }

    public void PlayerAttack(BunnyMessage<float> message)
    {
        if (stateMachine.currentState != DodgeState) {
            Health -= message.payload;
            BunnyEventManager.Instance.Fire<float>("OnPlayerHurt", new BunnyMessage<float>(Health, this));
        }
    }
}
