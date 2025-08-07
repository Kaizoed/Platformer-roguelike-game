using UnityEngine;

public class FireForceField : MonoBehaviour
{
    public float burnDuration = 5f;
    public float burnDamagePerSecond = 2f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Apply burn damage over time
            Damageable enemy = other.GetComponent<Damageable>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)(burnDamagePerSecond * Time.deltaTime), Vector2.zero, gameObject); // Add gameObject as third parameter

            }
        }
    }

    public void ActivateForceField()
    {
        // Activate the fire forcefield by enabling a visual effect (e.g., a particle system)
        // Enable a visual fire effect
        Debug.Log("Burn Forcefield Activated!");
    }

    public void DeactivateForceField()
    {
        // Deactivate the forcefield after burn time is over
        Debug.Log("Burn Forcefield Deactivated!");
    }
}
