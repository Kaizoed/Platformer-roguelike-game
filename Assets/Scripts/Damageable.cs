using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    public bool isPlayer = false; // true if this is the player, false if it's an enemy
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;
    public int regen = 0;
    public float regenTime = 1f; // in seconds

    [Header("Knockback Settings")]
    public float knockbackResistance = 1f;
    private Rigidbody2D rb;

    [Header("Optional World Health Bar (Prefab)")]
    [Tooltip("Assign only on the Enemy prefab: the green Fill sprite’s Transform")]
    public Transform worldHealthBarFill;
    private Vector3 worldOriginalFillScale;

    public Action<Damageable, GameObject> OnDeath;
    public Action<Damageable, GameObject> OnDamage;
    public Action<Damageable, GameObject> OnHeal;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Init world bar if assigned
        if (worldHealthBarFill != null)
        {
            worldOriginalFillScale = worldHealthBarFill.localScale;
            UpdateWorldBar();
        }

        StartCoroutine(StartRegen());
    }

    /// <summary> Call this from attacks to damage + knockback </summary>
    public void TakeDamage(int amount, Vector2 knockback, GameObject damager)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);

        // apply knockback
        if (rb != null)
            rb.AddForce(knockback / knockbackResistance, ForceMode2D.Impulse);

        // update whichever bar is present
        UpdateWorldBar();
        OnDamage?.Invoke(this, damager);

        if (currentHealth <= 0)
            Die(damager);
    }

    private void UpdateWorldBar()
    {
        if (worldHealthBarFill == null) return;

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
        if (isPlayer) Destroy(gameObject);
        else // destroy parent of this object instead
        {
            // If this is an enemy, destroy the parent GameObject
            Transform parent = transform.parent;
            if (parent != null)
            {
                Destroy(parent.gameObject);
            }
            else
            {
                Debug.LogWarning("Damageable: No parent found to destroy.");
            }
        }
    }

    IEnumerator StartRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenTime);
            
            if (regen == 0)
                yield return null;
            if (currentHealth == 0)
                break;

            currentHealth += regen;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            UpdateWorldBar();
            OnHeal?.Invoke(this, null); // no damager for healing
            yield return null;
        }

    }
}
