using UnityEngine;

public class FireForceField : MonoBehaviour
{
    public float burnDuration = 5f;
    public float burnDamagePerSecond = 2f;
    public ParticleSystem fireEffect; // Optional Particle System for fire effect

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

    public void ActivateForceField(float burnDamagePerSecond)
    {
        // Activate the fire forcefield by enabling a visual effect (e.g., a particle system)
        this.burnDamagePerSecond = burnDamagePerSecond; // Set burn damage per second dynamically
        if (fireEffect != null)
        {
            fireEffect.Play(); // Start the particle effect
        }
        Debug.Log("Burn Forcefield Activated!");
    }

    public void DeactivateForceField()
    {
        if (fireEffect != null)
        {
            fireEffect.Stop(); // Stop the particle effect
        }
        Debug.Log("Burn Forcefield Deactivated!");
    }
}
