using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI scoreText;
    public Image healthBarFill;

    private void Awake()
    {
        instance = this;
    }

    public void SetWave(int wave)
    {
        waveText.text = "WAVE: " + wave;
    }

    public void SetScore(int score)
    {
        scoreText.text = "SCORE: " + score;
    }

    public void SetHealth(float percent)
    {
        healthBarFill.fillAmount = percent;
    }
}