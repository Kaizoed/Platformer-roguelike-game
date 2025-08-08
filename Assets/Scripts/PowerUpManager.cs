using TMPro;
using UnityEngine;
using UnityEngine.UI;  // For UI Button components

public class PowerUpManager : MonoBehaviour
{
    [Header("PowerUp Settings")]
    public PowerUp[] availablePowerUps;  // Array of all available PowerUps
    public Button[] upgradeButtons;      // The buttons for the upgrades
    private PowerUp[] selectedPowerUps;  // Store the randomly selected PowerUps

    void Start()
    {
        // Initialize the upgrade buttons' onClick listeners
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int index = i;  // Capture the correct index in a local variable for the listener
            upgradeButtons[i].onClick.AddListener(() => ApplyUpgrade(selectedPowerUps[index]));
        }
    }

    // Randomly selects a specified number of PowerUps and assigns them to the buttons
    public void RandomizePowerUps(int count)
    {
        selectedPowerUps = GetRandomPowerUps(count);

        // Assign the PowerUp icons and names to the buttons
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            // Set the button icon
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
    public void ApplyUpgrade(PowerUp selectedPowerUp)
    {
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
        GameManager.Instance.HideUpgradePanel();
    }
}
