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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        if (player == null) return;

        // Move toward player
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;  // Move with Rigidbody2D

        // Try attacking
        TryAttack();
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); // Visualize attack range
    }
}
