using System;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Knockback Settings")]
    public float knockbackResistance = 1f;
    private Rigidbody2D rb;

    [Header("Optional UI Health Bar (Screen)")]
    [Tooltip("Assign only on the Player prefab")]
    public Slider uiHealthBar;

    [Header("Optional World Health Bar (Prefab)")]
    [Tooltip("Assign only on the Enemy prefab: the green Fill sprite’s Transform")]
    public Transform worldHealthBarFill;
    private Vector3 worldOriginalFillScale;

    public Action<Damageable, GameObject> OnDeath;
    private float lowHealthPercent = 0.1f;
    public Action OnLow;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        // Init UI bar if assigned
        if (uiHealthBar != null)
            uiHealthBar.value = 1f;

        // Init world bar if assigned
        if (worldHealthBarFill != null)
        {
            worldOriginalFillScale = worldHealthBarFill.localScale;
            UpdateWorldBar();
        }
    }

    /// <summary> Call this from attacks to damage + knockback </summary>
    public void TakeDamage(int amount, Vector2 knockback, GameObject damager)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);

        // apply knockback
        if (rb != null)
            rb.AddForce(knockback / knockbackResistance, ForceMode2D.Impulse);

        // update whichever bar is present
        if (uiHealthBar != null) UpdateUIBar();
        if (worldHealthBarFill != null) UpdateWorldBar();

        if (currentHealth <= maxHealth * lowHealthPercent)
            OnLow?.Invoke();
        if (currentHealth <= 0)
            Die(damager);
    }

    private void UpdateUIBar()
    {
        float ratio = (float)currentHealth / maxHealth;
        uiHealthBar.value = ratio;
    }

    private void UpdateWorldBar()
    {
        float ratio = (float)currentHealth / maxHealth;
        worldHealthBarFill.localScale = new Vector3(
            worldOriginalFillScale.x * ratio,
            worldOriginalFillScale.y,
            worldOriginalFillScale.z
        );
    }

    private void Die(GameObject killer)
    {
        OnDeath?.Invoke(this, killer);
        Destroy(gameObject);
    }
}
