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
        int[] validEvenNumbers = { -8, -6, -4, -2, 0, 2, 4, 6 };
        int x = validEvenNumbers[Random.Range(0, validEvenNumbers.Length)];
        return new Vector3(x, 0, 8);
    }

}
