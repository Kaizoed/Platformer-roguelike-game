using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WaveManager : MonoBehaviour
{
    [Header("What to Spawn")]
    public GameObject enemyPrefab;
    [Tooltip("How many enemies to spawn immediately on start")]
    public int spawnCountInit = 5;
    private int spawnCountCurr;
    public float spawnMultPerLevel = 1.5f;

    [Header("Ground Bounds")]
    [Tooltip("The ground collider defines the X range and spawn Y")]
    public Collider2D groundCollider;
    [Tooltip("Padding so enemies don't stand half off the edge")]
    public float horizontalPadding = 0.5f;

    private float minX, maxX, spawnY;

    public List<GameObject> spawnedEnemies = new();

    void Start()
    {
        spawnCountCurr = spawnCountInit;
        if (enemyPrefab == null || groundCollider == null)
        {
            Debug.LogError("EnemySpawner: assign both an enemyPrefab and groundCollider.", this);
            return;
        }

        StartSpawn();
    }

    private void StartSpawn()
    {
        // Compute spawn bounds from the ground’s collider
        Bounds bounds = groundCollider.bounds;
        minX = bounds.min.x + horizontalPadding;
        maxX = bounds.max.x - horizontalPadding;
        spawnY = bounds.max.y; // top of the ground

        // Spawn them all at once
        for (int i = 0; i < spawnCountInit; i++)
        {
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        float x = Random.Range(minX, maxX);
        Vector2 pos = new Vector2(x, spawnY);
        GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);

        Damageable damageable = enemy.GetComponent<Damageable>();
        damageable.OnDeath += OnEnemyDeath;

        spawnedEnemies.Add(enemy);
    }

    private void AdvanceLevel()
    {
        spawnCountCurr = (int) Mathf.Ceil(spawnCountCurr * spawnMultPerLevel); // always round up
        StartSpawn();
    }

    private void OnEnemyDeath(Damageable damageable)
    {
        spawnedEnemies.Remove(damageable.gameObject);

        if (spawnedEnemies.Count != 0)
            return;

        AdvanceLevel();
    }
}
