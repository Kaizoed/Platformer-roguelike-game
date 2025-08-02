using UnityEngine;
using UnityEngine.UI;  // Import to handle UI Image components

public class Damageable : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public System.Action OnDeath;

    [Header("Knockback Settings")]
    public float knockbackResistance = 1f;
    private Rigidbody2D rb;

    [Header("Health UI")]
    public Image healthBar;  // Reference to the Image component for the health bar

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        // Initialize UI
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;  // Set the initial fill amount
        }
    }

    public void TakeDamage(int amount, Vector2 knockback)
    {
        currentHealth -= amount;

        // Apply knockback if Rigidbody exists
        if (rb != null)
        {
            rb.AddForce(knockback / knockbackResistance, ForceMode2D.Impulse);
        }

        // Update the Health UI
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;  // Update the fill amount based on current health
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