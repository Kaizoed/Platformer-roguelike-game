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

    private void GrantXP(Damageable _, GameObject killer)
    {
        Debug.Log($"{name} died → awarding {xpValue} XP");

        // Get PlayerXP component
        var xpComp = killer.GetComponent<PlayerXP>();
        if (xpComp != null)
        {
            xpComp.GainXP(xpValue);
        }
        else
        {
            Debug.LogError("EnemyXPReward: Killer does not have PlayerXP component");
        }
    }
}
