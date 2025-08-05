using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class EnemyXPReward : MonoBehaviour
{
    [Tooltip("How much XP this enemy grants when killed")]
    public int xpValue = 10;

    private Damageable dmg;

    void Awake()
    {
        dmg = GetComponent<Damageable>();
        if (dmg != null)
        {
            dmg.OnDeath += GrantXP;
        }
        else
        {
            Debug.LogWarning($"{name}: No Damageable component found!");
        }
    }

    void OnDestroy()
    {
        if (dmg != null)
            dmg.OnDeath -= GrantXP;
    }

    private void GrantXP()
    {
        Debug.Log($"{name} died → awarding {xpValue} XP");

        // Find player by tag
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("EnemyXPReward: No GameObject with tag 'Player' found!");
            return;
        }

        // Get PlayerXP component
        var xpComp = player.GetComponent<PlayerXP>();
        if (xpComp != null)
        {
            xpComp.GainXP(xpValue);
        }
        else
        {
            Debug.LogError("EnemyXPReward: PlayerXP component missing on Player!");
        }
    }
}
