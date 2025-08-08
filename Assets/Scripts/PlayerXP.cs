using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TMP (TextMeshPro) support

public class PlayerXP : MonoBehaviour
{
    [Header("XP Settings")]
    public int currentXP = 0;
    public int xpToLevelUp = 100;

    [Header("UI Settings")]
    public Image xpFillImage;   // For updating the XP bar
    public GameObject upgradePanel;  // The upgrade panel to show when leveling up
    public TextMeshProUGUI levelUpText;  // The text component that shows the level-up message
    public Button upgradeButton1; // Optional Button 1
    public Button upgradeButton2; // Optional Button 2
    public Button upgradeButton3; // Optional Button 3

    public PowerUp[] powerUps; // Array of power-up options for the player to choose from

    public event System.Action<int, int> OnXPChanged;
    public event System.Action<int> OnXPGained;

    void Start()
    {
        // Ensure the panel is inactive at the start
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false);
        }
    }

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

        // Show the Upgrade Panel when leveling up
        ShowUpgradePanel();

        // Pause the game when the upgrade panel is shown
        PauseGame(true);
    }

    /// <summary> Shows the upgrade panel when the player levels up </summary>
    void ShowUpgradePanel()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true); // Display the upgrade panel
            levelUpText.text = "Level Up! Choose Your Upgrade!"; // Update text on panel

            // Set card icons for the buttons
            SetUpgradeCardIcons();

            // Optionally disable the upgrade buttons if needed
            upgradeButton1.interactable = true; // Enable Button1
            upgradeButton2.interactable = true; // Enable Button2
            upgradeButton3.interactable = true; // Enable Button3
        }
    }

    /// <summary> Sets the card icons to the buttons on the upgrade panel </summary>
    void SetUpgradeCardIcons()
    {
        // Randomly pick 3 power-ups from the available power-ups.
        PowerUp[] selectedPowerUps = GetRandomPowerUps(3);

        // Assign the power-up card icons to the buttons
        upgradeButton1.GetComponent<Image>().sprite = selectedPowerUps[0].cardIcon;
        upgradeButton2.GetComponent<Image>().sprite = selectedPowerUps[1].cardIcon;
        upgradeButton3.GetComponent<Image>().sprite = selectedPowerUps[2].cardIcon;

        // Remove any old listeners that might have been assigned previously
        upgradeButton1.onClick.RemoveAllListeners();
        upgradeButton2.onClick.RemoveAllListeners();
        upgradeButton3.onClick.RemoveAllListeners();

        // Dynamically add listeners to the buttons for each upgrade option
        upgradeButton1.onClick.AddListener(() => ApplyUpgrade1(selectedPowerUps[0]));
        upgradeButton2.onClick.AddListener(() => ApplyUpgrade2(selectedPowerUps[1]));
        upgradeButton3.onClick.AddListener(() => ApplyUpgrade3(selectedPowerUps[2]));
    }

    /// <summary> Randomly selects a given number of PowerUps from the available ones </summary>
    PowerUp[] GetRandomPowerUps(int count)
    {
        PowerUp[] selectedPowerUps = new PowerUp[count];
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, powerUps.Length);
            selectedPowerUps[i] = powerUps[randomIndex];
        }
        return selectedPowerUps;
    }

    /// <summary> Hides the upgrade panel after player chooses upgrades </summary>
    public void HideUpgradePanel()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(false); // Hide the upgrade panel after upgrade choices
        }

        // Unpause the game after upgrades are chosen
        PauseGame(false);
    }

    /// <summary> Pause or Unpause the game </summary>
    private void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0f : 1f; // Set the time scale to 0 to pause, or 1 to unpause
        Debug.Log($"Game Paused: {pause}");
    }

    /// <summary> Example upgrade logic for Button 1 </summary>
    public void ApplyUpgrade1(PowerUp selectedPowerUp)
    {
        Debug.Log($"{selectedPowerUp.powerUpName} Applied!");

        // Implement upgrade logic here, e.g., increase health, damage, etc.
        if (selectedPowerUp.powerUpType == PowerUpType.Health)
        {
            // Example: Increase health
            // playerHealth += selectedPowerUp.healthBoost;
        }
        else if (selectedPowerUp.powerUpType == PowerUpType.Damage)
        {
            // Example: Increase damage
            // playerDamage += selectedPowerUp.damageBoost;
        }
        // Add more logic based on the power-up type

        HideUpgradePanel();  // Hide the panel after the upgrade
    }

    public void ApplyUpgrade2(PowerUp selectedPowerUp)
    {
        Debug.Log($"{selectedPowerUp.powerUpName} Applied!");

        // Implement upgrade logic here
        if (selectedPowerUp.powerUpType == PowerUpType.Speed)
        {
            // Example: Increase speed
            // playerSpeed += selectedPowerUp.speedBoost;
        }
        // Add more logic based on the power-up type

        HideUpgradePanel();  // Hide the panel after the upgrade
    }

    public void ApplyUpgrade3(PowerUp selectedPowerUp)
    {
        Debug.Log($"{selectedPowerUp.powerUpName} Applied!");

        // Implement upgrade logic here
        if (selectedPowerUp.powerUpType == PowerUpType.Burn)
        {
            // Example: Activate burn effect
            // ActivateBurn(selectedPowerUp);
        }
        // Add more logic based on the power-up type

        HideUpgradePanel();  // Hide the panel after the upgrade
    }
}
