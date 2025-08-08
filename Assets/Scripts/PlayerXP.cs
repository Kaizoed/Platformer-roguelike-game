using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerXP : MonoBehaviour
{
    [Header("XP Settings")]
    public int currentXP = 0;
    public int xpToLevelUp = 100;

    [Header("UI Settings")]
    public Image xpFillImage;   // For updating the XP bar
    public GameObject upgradePanel;  // The upgrade panel to show when leveling up
    public TextMeshProUGUI levelUpText;  // TMP Text component for level-up message
    public Button upgradeButton1; // Optional Button 1
    public Button upgradeButton2; // Optional Button 2
    public Button upgradeButton3; // Optional Button 3

    private FireForceField fireForceField; // Reference to FireForceField

    public event Action<int, int> OnXPChanged;
    public event Action<int> OnXPGained;

    void Start()
    {
        // Ensure the panel is inactive at the start
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }

        fireForceField = UnityEngine.Object.FindAnyObjectByType<FireForceField>(); // Assign FireForceField reference
    }

    /// <summary>Adds XP, handles level‐up rollover, and refreshes the bar.</summary>
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

        // Show the Upgrade Panel when leveling up
        ShowUpgradePanel();

        // Call additional level-up logic here (e.g., increase stats, unlock abilities, etc.)
    }

    /// <summary> Shows the upgrade panel when the player levels up </summary>
    void ShowUpgradePanel()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true); // Display the upgrade panel
            levelUpText.text = "Level Up! Choose Your Upgrade!"; // Update text on panel

            // Optionally disable the upgrade buttons if needed
            upgradeButton1.interactable = true; // Enable Button1
            upgradeButton2.interactable = true; // Enable Button2
        }
    }

    /// <summary> Hides the upgrade panel after player chooses upgrades </summary>
    public void HideUpgradePanel()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false); // Hide the upgrade panel after upgrade choices
        }
    }

    /// <summary> Example upgrade logic for Button 1 </summary>
    public void ApplyUpgrade1()
    {
        Debug.Log("Upgrade 1 Applied!");
        // Implement upgrade logic here (e.g., increase health, damage, etc.)
        HideUpgradePanel();  // Hide the panel after upgrade selection
    }

    /// <summary> Example upgrade logic for Button 2 </summary>
    public void ApplyUpgrade2()
    {
        Debug.Log("Upgrade 2 Applied!");
        // Implement upgrade logic here (e.g., increase speed, etc.)
        HideUpgradePanel();  // Hide the panel after upgrade selection
    }

    /// <summary> Example upgrade logic for Burn upgrade (Activating the force field) </summary>
    public void ApplyUpgradeBurn(float burnDamagePerSecond)
    {
        if (fireForceField != null)
        {
            fireForceField.ActivateForceField(burnDamagePerSecond); // Activate fire force field on burn upgrade
            HideUpgradePanel();  // Hide the panel after selection
        }
    }
}
