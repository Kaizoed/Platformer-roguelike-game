using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public PlayerXP playerXP;
    [SerializeField] public PlayerController playerController;
    [SerializeField] public PlayerAttack playerAttack;
    [SerializeField] public Damageable playerHP;

    public static Action<Player> OnPlayerSpawn;

    private void Awake()
    {
        OnPlayerSpawn?.Invoke(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyUpgrade(PowerUp powerUp)
    {
        switch (powerUp.powerUpType)
        {
            case PowerUpType.HPRegen:
                playerHP.currentHealth += powerUp.healthBoost;
                playerHP.maxHealth += powerUp.healthBoost;
                playerHP.regen += powerUp.regen;
                break;
            case PowerUpType.Damage:
                playerAttack.attackDamage += powerUp.damageBoost;
                break;
            case PowerUpType.Speed:
                playerController.moveSpeed += powerUp.speedBoost;
                break;
            case PowerUpType.AttackSpeed:
                playerAttack.attackCooldown /= powerUp.attackspeedboost;
                break;
            case PowerUpType.Jump:
                playerController.jumpForce += powerUp.jumpBoost;
                break;

        }
    }
}
