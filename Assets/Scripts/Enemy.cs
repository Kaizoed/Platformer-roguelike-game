using UnityEngine;

public class EnemyFollowAndPatrol : MonoBehaviour
{
    public Transform player;
    public float followRange = 5f;
    public float followSpeed = 2f;
    public float patrolSpeed = 1.5f;

    public Transform leftPoint;
    public Transform rightPoint;

    private bool isGoingRight = true;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        // Get the animator and Rigidbody2D components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= followRange)
        {
            // Follow player
            FollowPlayer();
        }
        else
        {
            // Patrol between leftPoint and rightPoint
            Patrol();
        }
    }

    void FollowPlayer()
    {
        // Determine direction to player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move the enemy towards the player
        rb.linearVelocity = new Vector2(direction.x * followSpeed, rb.linearVelocity.y);

        // Set animation based on movement
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        // Flip the enemy's sprite based on movement direction
        FlipSprite(rb.linearVelocity.x);
    }

    void Patrol()
    {
        // Move the enemy between the patrol points
        if (isGoingRight)
        {
            rb.linearVelocity = new Vector2(patrolSpeed, rb.linearVelocity.y);

            if (transform.position.x >= rightPoint.position.x)
                isGoingRight = false;
        }
        else
        {
            rb.linearVelocity = new Vector2(-patrolSpeed, rb.linearVelocity.y);

            if (transform.position.x <= leftPoint.position.x)
                isGoingRight = true;
        }

        // Set animation based on movement
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        // Flip the enemy's sprite based on movement direction
        FlipSprite(rb.linearVelocity.x);
    }

    void FlipSprite(float velocityX)
    {
        // Flip sprite based on direction (left or right)
        if (velocityX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Facing right
        }
        else if (velocityX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Facing left
        }
    }
}