using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIWaveManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private PlayerXP playerXP; // used as scoring

    private int score = 0;

    private void Start()
    {
        BindToPlayer(GameManager.Instance.Player);
    }

    private void OnEnable()
    {
        WaveManager.OnNextWave += SetWave;
    }

    private void OnDestroy()
    {
        WaveManager.OnNextWave -= SetWave;
        playerXP.OnXPGained -= AddScore;
    }

    private void BindToPlayer(GameObject player)
    {
        // Get PlayerXP component
        var xpComp = player.GetComponent<PlayerXP>();
        if (xpComp == null)
        {
            Debug.LogError("UIWaveManager: PlayerXP component missing on Player!");
            return;
        }

        playerXP = xpComp;
        playerXP.OnXPGained += AddScore;
    }

    public void SetWave(int wave)
    {
        waveText.text = "Wave: " + wave;
    }

    private void AddScore(int amount)
    {
        score += amount;

        SetScore(score);
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}