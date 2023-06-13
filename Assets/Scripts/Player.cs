using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Cinemachine;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float playerJumpSpeed;
    [SerializeField] private float playerClimbSpeed;
    [SerializeField] private float playerGravityScale;
    [SerializeField] private float arrowDelayed;
    [SerializeField] private Vector2 deathKick = new Vector2();
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ladderLayer;
    [SerializeField] private LayerMask enemysLayer;
    [SerializeField] private LayerMask spikesLayer;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform bow;

    private Rigidbody2D playerPhysics2D;
    private Vector2 moveInput;
    private Animator animator;
    private CapsuleCollider2D playerBodyCollider;
    private BoxCollider2D playerFeetCollider;
    private bool isAlive = true;
    private const string ISWALKING = "IsWalking";
    private const string ISDEAD = "IsDead";
    private const string ISCLIMBING = "IsClimbing";
    private const string ISSHOOTING = "IsShooting";
    private CinemachineImpulseSource cinemachineImpulseSource;

    private void Awake()
    {
        playerPhysics2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Start()
    {
        playerGravityScale = playerPhysics2D.gravityScale;
    }

    private void Update()
    {
        if(!isAlive){return;}

        Run();
        FlipSprite2D();
        ClimbLadder();
        HitDetection();
    }

    private void OnMove(InputValue value)
    {
        if (!isAlive) { return; }

        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        if (value.isPressed)
        {
            if (playerFeetCollider.IsTouchingLayers(groundLayer))
            {
                playerPhysics2D.velocity += new Vector2(0f, playerJumpSpeed);
            }
        }
    }

    private void OnFire(InputValue value)
    {
        if (!isAlive) { return; }

        if (value.isPressed)
        {
            animator.SetTrigger(ISSHOOTING);
            Invoke("ArrowLose", arrowDelayed);
        }
    }

    private void ArrowLose()
    {
        if (playerPhysics2D.transform.localScale.x < Mathf.Epsilon)
        {
            Instantiate(arrow, bow.position, transform.rotation * Quaternion.Euler(0f,180f,0f));
        }

        else if(playerPhysics2D.transform.localScale.x > Mathf.Epsilon)
        {
            Instantiate(arrow, bow.position, transform.rotation);
        }
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerMoveSpeed, playerPhysics2D.velocity.y);
        playerPhysics2D.velocity = playerVelocity;
        bool Idling = Mathf.Abs(playerPhysics2D.velocity.x) > Mathf.Epsilon;
        animator.SetBool(ISWALKING, Idling);
    }

    private void ClimbLadder()
    {
        if (playerFeetCollider.IsTouchingLayers(ladderLayer))
        {
            Vector2 playerclimbVelocity = new Vector2(playerPhysics2D.velocity.x, moveInput.y * playerClimbSpeed);
            playerPhysics2D.velocity = playerclimbVelocity;
            playerPhysics2D.gravityScale = 0f;
            bool Idling = Mathf.Abs(playerPhysics2D.velocity.y) > Mathf.Epsilon;
            animator.SetBool(ISCLIMBING, Idling);
        }

        else
        {
            playerPhysics2D.gravityScale = playerGravityScale;
            animator.SetBool(ISCLIMBING, false);
        }
    }

    private void FlipSprite2D()
    {
        bool Idling = Mathf.Abs(playerPhysics2D.velocity.x) > Mathf.Epsilon;

        if(Idling)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerPhysics2D.velocity.x), 1f);
        }
    }

    private void HitDetection()
    {
        if (playerBodyCollider.IsTouchingLayers(enemysLayer) || playerFeetCollider.IsTouchingLayers(spikesLayer))
        {
            isAlive = false;
            animator.SetTrigger(ISDEAD);
            playerPhysics2D.velocity = deathKick;
            cinemachineImpulseSource.GenerateImpulse(1);
            FindObjectOfType<GameSession>().ProssesPlayerDeath();
        }
    }
}
