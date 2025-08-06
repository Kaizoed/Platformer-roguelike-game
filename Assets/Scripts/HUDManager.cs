using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private Slider uiHP;
    [SerializeField] private Slider uiXP;

    private Damageable playerHP;
    private PlayerXP playerXP;

    private void OnEnable()
    {
        PlayerController.OnPlayerSpawn += SetTarget;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerSpawn -= SetTarget;
        if (playerHP != null) playerHP.OnDamage -= UpdateHealthBar;
        if (playerXP != null) playerXP.OnXPChanged -= UpdateXPBar;
    }

    private void SetTarget(GameObject target)
    {
        if (target.TryGetComponent<Damageable>(out var _playerHP))
        {
            playerHP = _playerHP;
            playerHP.OnDamage += UpdateHealthBar;
            UpdateHealthBar(playerHP);
        }

        if (target.TryGetComponent<PlayerXP>(out var _playerXP))
        {
            playerXP = _playerXP;
            playerXP.OnXPChanged += UpdateXPBar;
            UpdateXPBar(playerXP.currentXP, playerXP.xpToLevelUp);
        }
    }

    void UpdateHealthBar(Damageable damageable, GameObject _ = null)
    {
        int currHP = damageable.currentHealth;
        int maxHP = damageable.maxHealth;
        uiHP.value = (float)currHP / maxHP; // cast one to float to get float result
    }

    void UpdateXPBar(int currXP, int maxXP)
    {
        uiXP.value = (float)currXP / maxXP; // cast one to float to get float result
    }
}