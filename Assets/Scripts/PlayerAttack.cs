using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 25;
    public float attackCooldown = 0.5f;
    public float knockbackForce = 5f;

    private float nextAttackTime = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0)) // Left click
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
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

        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Damageable dmg = enemy.GetComponent<Damageable>();
            if (dmg != null)
            {
                Vector2 knockbackDir = (enemy.transform.position - transform.position).normalized;
                dmg.TakeDamage(attackDamage, knockbackDir * knockbackForce);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}