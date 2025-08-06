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
        SetWave(WaveManager.instance.wave); // get wave in case it has already started;
        WaveManager.instance.OnNextWave += SetWave;
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("UIWaveManager: No GameObject with tag 'Player' found!");
            return;
        }

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

    private void OnDestroy()
    {
        WaveManager.instance.OnNextWave -= SetWave;
        playerXP.OnXPGained -= AddScore;
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