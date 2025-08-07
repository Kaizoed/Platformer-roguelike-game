using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Ensure TextMeshPro is imported for UI text
using System;
using System.Collections.Generic;

public class PlayerXP : MonoBehaviour
{
    [Header("XP Settings")]
    public int currentXP = 0;
    public int xpToLevelUp = 100;

    [Header("UI Settings")]
    public Image xpFillImage;   // For updating the XP bar
    public GameObject upgradePanel;  // The upgrade panel to show when leveling up
    public TMP_Text LevelUpText;  // The TMP_Text component for the level-up message (changed to TMP_Text)
    public Button upgradeButton1; // Button 1 for upgrade
    public Button upgradeButton2; // Button 2 for upgrade
    public Button upgradeButton3; // Button 3 for upgrade

    [Header("Player Stats")]
    public float playerHealth = 100f;   // Player's health
    public float playerDamage = 10f;    // Player's damage
    public float playerSpeed = 5f;      // Player's speed
    public float playerJump = 5f;       // Player's jump height

    [Header("Power-Ups")]
    public List<PowerUp> allPowerUps;  // List of all available power-ups
    private List<PowerUp> randomPowerUps = new List<PowerUp>();  // Holds the 3 randomly selected power-ups

    [Header("Fire ForceField")]
    public FireForceField fireForceField;  // Reference to the FireForceField script

    public event Action<int, int> OnXPChanged;
    public event Action<int> OnXPGained;

    void Start()
    {
        // Ensure the panel is inactive at the start
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }
    }

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

        // Update the XP bar
        UpdateXPBar();

        OnXPChanged?.Invoke(currentXP, xpToLevelUp);
    }

    void LevelUp()
    {
        Debug.Log("[PlayerXP] Leveled Up!");

        // Show the Upgrade Panel when leveling up
        ShowUpgradePanel();
    }

    void UpdateXPBar()
    {
        if (xpFillImage != null)
        {
            xpFillImage.fillAmount = (float)currentXP / xpToLevelUp;
        }
    }

    void ShowUpgradePanel()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true); // Display the upgrade panel
            LevelUpText.text = "Level Up! Choose Your Upgrade!"; // Update text on panel

            // Randomly select 3 upgrades from the list of power-ups
            SelectRandomPowerUps();

            // Assign the selected power-ups to the buttons
            AssignPowerUpsToButtons();
        }
    }

    void SelectRandomPowerUps()
    {
        randomPowerUps.Clear();

        List<PowerUp> tempList = new List<PowerUp>(allPowerUps);

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, tempList.Count);
            randomPowerUps.Add(tempList[randomIndex]);
            tempList.RemoveAt(randomIndex);  // Remove the selected power-up so we don't select it again
        }
    }

    void AssignPowerUpsToButtons()
    {
        // Button 1
        upgradeButton1.GetComponentInChildren<TMP_Text>().text = randomPowerUps[0].powerUpName; // Use TMP_Text for Button1
        upgradeButton1.onClick.AddListener(() => ApplyUpgrade(randomPowerUps[0]));
        upgradeButton1.interactable = true; // Enable Button1

        // Button 2
        upgradeButton2.GetComponentInChildren<TMP_Text>().text = randomPowerUps[1].powerUpName; // Use TMP_Text for Button2
        upgradeButton2.onClick.AddListener(() => ApplyUpgrade(randomPowerUps[1]));
        upgradeButton2.interactable = true; // Enable Button2

        // Button 3
        upgradeButton3.GetComponentInChildren<TMP_Text>().text = randomPowerUps[2].powerUpName; // Use TMP_Text for Button3
        upgradeButton3.onClick.AddListener(() => ApplyUpgrade(randomPowerUps[2]));
        upgradeButton3.interactable = true; // Enable Button3
    }

    public void ApplyUpgrade(PowerUp selectedPowerUp)
    {
        // Apply the corresponding effect depending on the type of power-up selected
        switch (selectedPowerUp.powerUpType)
        {
            case PowerUpType.Health:
                playerHealth += selectedPowerUp.healthBoost;
                break;

            case PowerUpType.Damage:
                playerDamage += selectedPowerUp.damageBoost;
                break;

            case PowerUpType.Speed:
                playerSpeed += selectedPowerUp.speedBoost;
                break;

            case PowerUpType.Burn:
                // Apply Burn upgrade logic
                fireForceField.ActivateForceField(selectedPowerUp.burnDamagePerSecond, selectedPowerUp.burnDuration);
                break;

            case PowerUpType.Jump:
                playerJump += selectedPowerUp.speedBoost;
                break;

                // Add other cases for any new power-ups (e.g., attack speed, HP regen, etc.)
        }

        Debug.Log($"Applied upgrade: {selectedPowerUp.powerUpName}");
        HideUpgradePanel();
    }

    void HideUpgradePanel()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false); // Hide the upgrade panel after upgrade choices
        }
    }
}
