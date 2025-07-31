using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [Header("Health")]
    public RectTransform healthFG;
    public float maxHealth = 100;
    public float currentHealth = 100;

    [Header("XP")]
    public RectTransform xpFG;
    public float maxXP = 100;
    public float currentXP = 0;

    private float healthFullWidth;
    private float xpFullWidth;

    void Start()
    {
        healthFullWidth = healthFG.sizeDelta.x;
        xpFullWidth = xpFG.sizeDelta.x;

        UpdateHealthBar();
        UpdateXPBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        UpdateHealthBar();
    }

    public void GainXP(float amount)
    {
        currentXP = Mathf.Clamp(currentXP + amount, 0, maxXP);
        UpdateXPBar();
    }

    void UpdateHealthBar()
    {
        float width = (currentHealth / maxHealth) * healthFullWidth;
        healthFG.sizeDelta = new Vector2(width, healthFG.sizeDelta.y);
    }

    void UpdateXPBar()
    {
        float width = (currentXP / maxXP) * xpFullWidth;
        xpFG.sizeDelta = new Vector2(width, xpFG.sizeDelta.y);
    }
}