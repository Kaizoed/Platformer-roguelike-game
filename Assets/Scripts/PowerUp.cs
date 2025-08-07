using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "PowerUp")]
public class PowerUp : ScriptableObject
{
    [Header("Basic PowerUp Info")]
    public string powerUpName;       // Name of the power-up (e.g., "Increase Health")
    public string description;       // Description of the power-up (e.g., "Increase health by 50 points.")

    [Header("Power-Up Type")]
    public PowerUpType powerUpType;  // Enum to define the type of the power-up (e.g., health, damage, burn, etc.)

    [Header("Stats Modifiers")]
    public int healthBoost = 0;      // Amount to boost health (only used if powerUpType is Health or HP Regen)
    public int damageBoost = 0;      // Amount to boost damage (used for Attack power-ups)
    public float speedBoost = 0f;    // Amount to boost speed (used for Speed or Jump power-ups)
    public float burnDamagePerSecond = 0f; // Only used for Burn power-up to deal damage over time

    [Header("Burn Specific Settings")]
    public float burnDuration = 0f;  // How long the burn lasts (only used for Burn type)

    // Add more fields depending on the power-up type
}

public enum PowerUpType
{
    Health,
    Damage,
    Speed,
    Burn,
    AttackSpeed,
    Jump,
    HPRegen,
    Back
}
