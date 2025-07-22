using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // You can now assign multiple types!
    public Transform player;
    public float spawnInterval = 5f;
    public int maxEnemies = 5;

    private float timer;
    private int currentEnemies = 0;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // Randomly select a prefab from the array
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject prefab = enemyPrefabs[randomIndex];

        GameObject enemy = Instantiate(prefab, transform.position, Quaternion.identity);

        // Check if it has a FlyingEnemyAI or Ground script and assign player
        if (enemy.TryGetComponent(out FlyingEnemy flyingAI))
        {
            flyingAI.player = player;
        }
        else if (enemy.TryGetComponent(out EnemyFollowAndPatrol groundAI))
        {
            groundAI.player = player;
        }

        currentEnemies++;
    }
}