using TMPro;
using UnityEngine;
using UnityEngine.UI;  // For UI Button components

public class PowerUpManager : MonoBehaviour
{
    [Header("PowerUp Settings")]
    public PowerUp[] availablePowerUps;  // Array of all available PowerUps
    public Button[] upgradeButtons;      // The buttons for the upgrades
    public TextMeshProUGUI levelUpText;  // The text component that shows the level-up message

    private PowerUp[] selectedPowerUps;  // Store the randomly selected PowerUps
    private PlayerXP playerXP;
    void Start()
    {
        // Initialize the upgrade buttons' onClick listeners
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int index = i;  // Capture the correct index in a local variable for the listener
            upgradeButtons[i].onClick.AddListener(() => ApplyUpgrade(index));
        }

        playerXP = GameManager.Instance.Player?.GetComponent<PlayerXP>();

        if (playerXP == null)
        {
            Debug.LogError("Can't find PlayerXP");
            return;
        }

        playerXP.OnLevelUp += ShowUpgradePanel;

        HideUpgradePanel();
    }

    private void OnDestroy()
    {
        playerXP.OnLevelUp -= ShowUpgradePanel;
    }

    /// <summary> Shows the upgrade panel when the player levels up </summary>
    void ShowUpgradePanel()
    {
        if (!gameObject.activeSelf) // only pause once for upgrade panel
        {
            // Pause the game when showing upgrades
            GameManager.Instance.PauseGame(true);
        }

        gameObject.SetActive(true); // Display the upgrade panel
        levelUpText.text = "Level Up! Choose Your Upgrade!"; // Update text on panel

        // Set card icons for the buttons
        SetUpgradeCardIcons();
    }

    /// <summary> Hides the upgrade panel after player chooses upgrades </summary>
    public void HideUpgradePanel()
    {
        if (gameObject.activeSelf) // only unpause once for upgrade panel
        {
            // Unpause the game after upgrades are chosen
            GameManager.Instance.PauseGame(false);
        }
        gameObject.SetActive(false); // Hide the upgrade panel after upgrade choices

    }

    /// <summary> Sets the card icons to the buttons on the upgrade panel </summary>
    void SetUpgradeCardIcons()
    {
        // Randomly pick 3 power-ups from the available power-ups.
        selectedPowerUps = GetRandomPowerUps(3);

        // Assign the power-up card icons to the buttons
        for (int i = 0; i < selectedPowerUps.Length; i++)
        {
            upgradeButtons[i].GetComponent<Image>().sprite = selectedPowerUps[i].cardIcon;
            // Set the button text
            TextMeshProUGUI buttonText = upgradeButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = selectedPowerUps[i].powerUpName;
            }
        }
    }

    // Get random PowerUps
    private PowerUp[] GetRandomPowerUps(int count)
    {
        PowerUp[] chosenPowerUps = new PowerUp[count];
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, availablePowerUps.Length);
            chosenPowerUps[i] = availablePowerUps[randomIndex];
        }
        return chosenPowerUps;
    }

    // Apply the selected PowerUp when the player clicks a button
    public void ApplyUpgrade(int i)
    {
        PowerUp selectedPowerUp = selectedPowerUps[i];
        Debug.Log($"{selectedPowerUp.powerUpName} Applied!");

        // Example: Implement what happens when the upgrade is chosen
        // Here, you can apply the actual game logic, e.g., increase health, speed, etc.
        // If the PowerUp is for increasing health, for example:
        if (selectedPowerUp.powerUpType == PowerUpType.Health)
        {
            // Increase health logic here
            Debug.Log($"Health increased by {selectedPowerUp.healthBoost}");
        }
        else if (selectedPowerUp.powerUpType == PowerUpType.Damage)
        {
            // Increase damage logic here
            Debug.Log($"Damage increased by {selectedPowerUp.damageBoost}");
        }
        // Add more conditions based on your PowerUp types

        // Close the upgrade panel
        HideUpgradePanel();
    }
}
