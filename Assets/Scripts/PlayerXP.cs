using UnityEngine;
using UnityEngine.UI;

public class PlayerXP : MonoBehaviour
{
    public Image xpFillImage;
    public int currentXP = 0;
    public int xpToLevelUp = 100;

    void UpdateXPBar()
    {
        xpFillImage.fillAmount = (float)currentXP / xpToLevelUp;
    }

    public void GainXP(int amount)
    {
        currentXP += amount;
        if (currentXP >= xpToLevelUp)
        {
            currentXP -= xpToLevelUp;
            LevelUp();
        }
        UpdateXPBar();
    }

    void LevelUp()
    {
        // Trigger your level-up logic here
        Debug.Log("Leveled Up!");
    }
}