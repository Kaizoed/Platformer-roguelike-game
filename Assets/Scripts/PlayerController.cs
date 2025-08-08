using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public static Action<GameObject> OnPlayerSpawn;

    private void Awake()
    {
        OnPlayerSpawn?.Invoke(gameObject);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        // ? Apply movement
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // ? Flip sprite based on direction
        FlipSprite(move);

        // ? Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ? Set Animator Parameters
        animator.SetBool("isWalking", move != 0);
        animator.SetBool("isJumping", !isGrounded && rb.linearVelocity.y > 0.1f);
        animator.SetBool("isFalling", !isGrounded && rb.linearVelocity.y < -0.1f);

        // ? Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FlipSprite(float vx)
    {
        if (vx > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (vx < 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }

    // Draw ground check gizmo in Scene view
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}