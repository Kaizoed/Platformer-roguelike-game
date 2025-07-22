using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    public int attackDamage = 10;
    public float attackCooldown = 1f;
    public float knockbackForce = 5f; // knockback force

    private float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            Damageable dmg = player.GetComponent<Damageable>();
            if (dmg != null)
            {
                Vector2 knockbackDir = (player.transform.position - transform.position).normalized;
                Vector2 knockback = knockbackDir * knockbackForce;
                dmg.TakeDamage(attackDamage, knockback);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}