using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    [SerializeField] private float attackRangeScaled = 0.5f;
    public LayerMask playerLayer;
    public int attackDamage = 10;
    public float attackCooldown = 1f;
    public float knockbackForce = 5f; // knockback force

    private float nextAttackTime = 0f;
    private Animator animator;

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
        SetAttackRange();
    }

    void Update()
    {
        // Check if cooldown has passed before allowing another attack
        if (Time.time >= nextAttackTime)
        {
            Ready();
            nextAttackTime = Time.time + attackCooldown;  // Reset the cooldown
        }
    }

    void Ready()
    {
        // Trigger the attack animation
        animator.SetTrigger("Attack");
    }

    void Attack()
    {
        // Detect players within attack range
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRangeScaled, playerLayer);

        if (hitPlayers.Length > 0)
        {
            Debug.Log("Player detected in attack range!");
        }

        foreach (Collider2D player in hitPlayers)
        {
            Damageable dmg = player.GetComponent<Damageable>();
            if (dmg != null)
            {
                // Calculate knockback direction and apply it
                Vector2 knockbackDir = (player.transform.position - transform.position).normalized;
                Vector2 knockback = knockbackDir * knockbackForce;

                // Apply damage and knockback to the player
                dmg.TakeDamage(attackDamage, knockback, gameObject);
                Debug.Log($"Damage applied to {player.name} with knockback!");
            }
        }
    }

    void SetAttackRange()
    {
        attackRangeScaled = attackRange * transform.localScale.x; // Scale the attack range based on enemy size
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        SetAttackRange();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRangeScaled);  // Visualize attack range in the scene view
    }
}