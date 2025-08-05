using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WaveManager : MonoBehaviour
{
    [Header("What to Spawn")]
    public GameObject enemyPrefab;
    [Tooltip("How many enemies to spawn immediately on start")]
    public int spawnCount = 5;

    [Header("Ground Bounds")]
    [Tooltip("The ground collider defines the X range and spawn Y")]
    public Collider2D groundCollider;
    [Tooltip("Padding so enemies don't stand half off the edge")]
    public float horizontalPadding = 0.5f;

    private float minX, maxX, spawnY;

    void Start()
    {
        if (enemyPrefab == null || groundCollider == null)
        {
            Debug.LogError("EnemySpawner: assign both an enemyPrefab and groundCollider.", this);
            return;
        }

        // Compute spawn bounds from the ground’s collider
        Bounds bounds = groundCollider.bounds;
        minX = bounds.min.x + horizontalPadding;
        maxX = bounds.max.x - horizontalPadding;
        spawnY = bounds.max.y; // top of the ground

        // Spawn them all at once
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        float x = Random.Range(minX, maxX);
        Vector2 pos = new Vector2(x, spawnY);
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}
