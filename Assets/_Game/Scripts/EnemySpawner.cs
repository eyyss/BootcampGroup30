using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate;
    private float spawnTimer;
    public GameObject enemyPrefab;
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }
    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, RandomSpawnPos(), enemyPrefab.transform.rotation);
    }
    public Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-8, 6), 0, 8);
    }

}
