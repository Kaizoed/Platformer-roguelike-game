using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public System.Action OnDeath;

    [Header("Knockback Settings")]
    public float knockbackResistance = 1f; // You can use this to tweak knockback
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int amount, Vector2 knockback)
    {
        currentHealth -= amount;

        // Apply knockback if Rigidbody exists
        if (rb != null)
        {
            rb.AddForce(knockback / knockbackResistance, ForceMode2D.Impulse);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnDeath?.Invoke();
        // Destroy or disable the object
        Destroy(gameObject);
    }
}