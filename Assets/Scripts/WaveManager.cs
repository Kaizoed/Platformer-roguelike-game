using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public int enemyCount;
        public float spawnInterval;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;

    private int currentWaveIndex = 0;
    private int enemiesSpawned = 0;
    private float spawnTimer = 0f;
    private float waveCooldown = 3f; // wait between waves
    private float waveTimer = 0f;
    private bool waitingForNextWave = false;

    void Update()
    {
        if (currentWaveIndex >= waves.Length) return;

        Wave currentWave = waves[currentWaveIndex];

        if (!waitingForNextWave)
        {
            spawnTimer += Time.deltaTime;

            if (enemiesSpawned < currentWave.enemyCount && spawnTimer >= currentWave.spawnInterval)
            {
                SpawnEnemy(currentWave.enemyPrefab);
                spawnTimer = 0f;
                enemiesSpawned++;
            }

            if (enemiesSpawned >= currentWave.enemyCount)
            {
                waitingForNextWave = true;
                waveTimer = 0f;
            }
        }
        else
        {
            waveTimer += Time.deltaTime;
            if (waveTimer >= waveCooldown)
            {
                currentWaveIndex++;
                enemiesSpawned = 0;
                waitingForNextWave = false;
            }
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}