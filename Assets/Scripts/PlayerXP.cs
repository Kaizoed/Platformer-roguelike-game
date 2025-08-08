using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerXP : MonoBehaviour
{
    [Header("XP Settings")]
    public int currentXP = 0;
    public int xpToLevelUp = 100;

    public event System.Action<int, int> OnXPChanged;
    public event System.Action<int> OnXPGained;
    public event System.Action OnLevelUp;

    /// <summary>Adds XP, handles level-up rollover, and refreshes the bar.</summary>
    public void GainXP(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning($"PlayerXP.GainXP called with non‐positive amount: {amount}");
            return;
        }

        OnXPGained?.Invoke(amount);

        Debug.Log($"[PlayerXP] Gaining {amount} XP. Before: {currentXP}/{xpToLevelUp}");
        currentXP += amount;

        if (currentXP >= xpToLevelUp)
        {
            currentXP -= xpToLevelUp;
            LevelUp();
        }

        OnXPChanged?.Invoke(currentXP, xpToLevelUp);
    }

    /// <summary>Called when enough XP is collected to level up.</summary>
    void LevelUp()
    {
        Debug.Log("[PlayerXP] Leveled Up!");
        OnLevelUp?.Invoke();
    }
}
