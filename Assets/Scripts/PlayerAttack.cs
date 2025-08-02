using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 25;
    public float attackCooldown = 0.5f;  // Cooldown between attacks
    public float knockbackForce = 5f;    // Knockback force when enemy is hit

    private float nextAttackTime = 0f;   // Track when the player can attack again
    private Animator animator;           // Animator reference for animation control

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the cooldown has passed and if the left mouse button is clicked
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0)) // Left mouse button (click)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;  // Reset attack cooldown
            }
        }
    }

    void Attack()
    {
        // Play attack animation
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Detect enemies within the attack range using OverlapCircleAll
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Damageable dmg = enemy.GetComponent<Damageable>();
            if (dmg != null)
            {
                // Calculate knockback direction (away from the player)
                Vector2 knockbackDir = (enemy.transform.position - transform.position).normalized;

                // Apply damage and knockback to the enemy
                dmg.TakeDamage(attackDamage, knockbackDir * knockbackForce);
            }
        }
    }

    // Visualize the attack range in the Scene view with Gizmos
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);  // Show the range as a yellow sphere
    }
}