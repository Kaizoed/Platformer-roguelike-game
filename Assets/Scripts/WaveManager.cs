using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    public int wave = 0;
    public float timeBeforeWave = 3;
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

    public System.Action<int> OnNextWave;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        instance = this;
    }

    void Start()
    {
        spawnCountCurr = spawnCountInit;
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

        StartCoroutine(StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        wave++;
        OnNextWave?.Invoke(wave);
        yield return new WaitForSeconds(timeBeforeWave);

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
        StartCoroutine(StartSpawn());
    }

    private void OnEnemyDeath(Damageable damageable, GameObject killer)
    {
        spawnedEnemies.Remove(damageable.gameObject);

        if (spawnedEnemies.Count != 0)
            return;

        AdvanceLevel();
    }
}
