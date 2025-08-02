using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int playerXP = 0;
    public int xpToLevelUp = 100;
    public int playerLevel = 1;
    public GameObject levelUpPanel;

    public void AddXP(int amount)
    {
        playerXP += amount;
        if (playerXP >= xpToLevelUp)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        playerLevel++;
        playerXP = 0;
        xpToLevelUp += 50; // Increase difficulty
        Time.timeScale = 0f; // Pause the game
        levelUpPanel.SetActive(true);
    }

    public void ChoosePowerUp()
    {
        // You can expand this to take parameters
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
        // Apply power-up effect
    }
}