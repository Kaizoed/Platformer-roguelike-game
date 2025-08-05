using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerXP : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("The UI Image component used as the XP fill bar (must be of Type=Filled)")]
    public Image xpFillImage;

    [Header("XP Settings")]
    public int currentXP = 0;
    public int xpToLevelUp = 100;

    /// <summary>
    /// Fired whenever XP changes: (currentXP, xpToLevelUp)
    /// </summary>
    public event Action<int, int> OnXPChanged;

    void Start()
    {
        if (xpFillImage == null)
            Debug.LogError("PlayerXP: xpFillImage is not assigned!", this);
        else
            UpdateXPBar();
    }

    /// <summary>Updates the UI fill. Clamps between 0 and 1.</summary>
    void UpdateXPBar()
    {
        if (xpFillImage == null) return;

        float ratio = Mathf.Clamp01((float)currentXP / xpToLevelUp);
        xpFillImage.fillAmount = ratio;

        Debug.Log($"[PlayerXP] Bar updated: {currentXP}/{xpToLevelUp} ({ratio * 100:F1}%)");
        OnXPChanged?.Invoke(currentXP, xpToLevelUp);
    }

    /// <summary>Adds XP, handles level‐up rollover, and refreshes the bar.</summary>
    public void GainXP(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning($"PlayerXP.GainXP called with non‐positive amount: {amount}");
            return;
        }

        Debug.Log($"[PlayerXP] Gaining {amount} XP. Before: {currentXP}/{xpToLevelUp}");
        currentXP += amount;

        if (currentXP >= xpToLevelUp)
        {
            currentXP -= xpToLevelUp;
            LevelUp();
        }

        UpdateXPBar();
    }

    /// <summary>Called when enough XP is collected to level up.</summary>
    void LevelUp()
    {
        Debug.Log("[PlayerXP] Leveled Up!");
        // TODO: add your level‐up logic here (increase stats, play VFX, etc.)
    }
}
