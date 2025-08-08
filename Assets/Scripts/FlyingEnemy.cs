using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 1.5f;

    [Header("Attack Settings")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public int attackDamage = 10;
    public float attackCooldown = 1.5f;

    private float nextAttackTime = 0f;

    // Animator for attack animations
    public Animator animator;

    private Rigidbody2D rb;

    [Header("Patrol Settings")]
    public Transform leftPoint;  // Left patrol point
    public Transform rightPoint; // Right patrol point
    private bool isGoingRight = true;  // Direction flag for patrol

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // If these were never assigned in the Inspector (because you're using a prefab),
        // find them by tag or name in the scene:
        if (player == null)
        {
            Player _player = GameManager.Instance.Player;
            if (_player != null) player = _player.transform;
        }

        // Patrol logic: Move back and forth between left and right points
        Patrol();

        // Try attacking
        TryAttack();
    }

    void Patrol()
    {
        float direction = isGoingRight ? 1f : -1f; // Determines if moving right or left

        // Move the enemy in the chosen direction
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        // Check if enemy has reached either the left or right point
        if (transform.position.x >= rightPoint.position.x && isGoingRight)
        {
            isGoingRight = false; // Start moving left
        }
        else if (transform.position.x <= leftPoint.position.x && !isGoingRight)
        {
            isGoingRight = true; // Start moving right
        }
    }

    void TryAttack()
    {
        if (Time.time < nextAttackTime) return;

        // Trigger attack animation
        animator.SetTrigger("Attack");  // Trigger attack animation

        // Detect players within the attack range
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D hit in hitPlayers)
        {
            Damageable dmg = hit.GetComponent<Damageable>();
            if (dmg != null)
            {
                Vector2 knockbackDir = hit.transform.position - transform.position;
                dmg.TakeDamage(attackDamage, knockbackDir, gameObject);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        // Visualize attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        // Visualize patrol points in Scene view (left and right patrol points)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(leftPoint.position, 0.2f);  // Left patrol point
        Gizmos.DrawWireSphere(rightPoint.position, 0.2f); // Right patrol point
    }
}
