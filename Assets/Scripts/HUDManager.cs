using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private float lowHPThreshold = 0.25f; // 25% health or less
    [SerializeField] private Slider uiHP;
    [SerializeField] private Slider uiXP;
    [SerializeField] private GameObject hudLowHP;

    private Damageable playerHP;
    private PlayerXP playerXP; // for xp bar and for scoring

    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    private void Start()
    {
        hudLowHP.SetActive(false);

        SetTarget(GameManager.Instance.Player);
    }

    private void OnEnable()
    {
        WaveManager.OnNextWave += SetWave;
    }

    private void OnDisable()
    {
        if (playerHP != null) playerHP.OnDamage -= UpdateHealth;
        if (playerXP != null) playerXP.OnXPChanged -= UpdateXPBar;
        WaveManager.OnNextWave -= SetWave;
    }

    private void OnDestroy()
    {
        playerXP.OnXPGained -= AddScore;
    }

    private void SetTarget(GameObject target)
    {
        if (target == null)
        {
            Debug.LogError("Target is null");
            return;
        }
        
        if (target.TryGetComponent<Damageable>(out var _playerHP))
        {
            playerHP = _playerHP;
            playerHP.OnDamage += UpdateHealth;
            UpdateHealth(playerHP);
        }

        if (target.TryGetComponent<PlayerXP>(out var _playerXP))
        {
            playerXP = _playerXP;
            playerXP.OnXPChanged += UpdateXPBar;
            playerXP.OnXPGained += AddScore;
            UpdateXPBar(playerXP.currentXP, playerXP.xpToLevelUp);
        }
    }

    void UpdateHealth(Damageable damageable, GameObject _ = null)
    {
        int currHP = damageable.currentHealth;
        int maxHP = damageable.maxHealth;
        float healthRatio = (float)currHP / maxHP;
        uiHP.value = healthRatio; // cast one to float to get float result
        if ( healthRatio <= lowHPThreshold)
        {
            hudLowHP.SetActive(true);
        }
        else
        {
            hudLowHP.SetActive(false);
        }
    }

    void UpdateXPBar(int currXP, int maxXP)
    {
        uiXP.value = (float)currXP / maxXP; // cast one to float to get float result
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