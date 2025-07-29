using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Singelton;
    public LevelData levelData;
    public Transform spawnPoint;

    private int currentWave = 0;
    private List<GameObject> aliveEnemies = new List<GameObject>();

    public int preparationDuration = 4;


    void Awake()
    {
        Singelton = this;
    }

    void Start()
    {
        StartCoroutine(PreparationAndSpawn());
    }

    IEnumerator PreparationAndSpawn()
    {

        UIManager.Singelton.OpenPreparationPanel();
        yield return new WaitForSeconds(preparationDuration);
        UIManager.Singelton.ClosePreparationPanel();

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave < levelData.waves.Count)
        {
            if (currentWave == levelData.waves.Count - 1)
            {
                Debug.Log("Final Wave!");
                UIManager.Singelton.OpenFinalWavePanel();
                // UI: Show final wave text
            }

            yield return StartCoroutine(SpawnWave(levelData.waves[currentWave]));
            yield return new WaitUntil(() => aliveEnemies.Count == 0);

            currentWave++;
            yield return new WaitForSeconds(levelData.timeBetweenWaves);
        }

        Debug.Log("All waves completed!");
        UIManager.Singelton.OpenVictoryPanel();
    }

    IEnumerator SpawnWave(Wave wave)
    {
        foreach (var enemyInfo in wave.enemies)
        {
            for (int i = 0; i < enemyInfo.count; i++)
            {
                if (Random.value <= enemyInfo.spawnChance)
                {
                    GameObject enemy = Instantiate(enemyInfo.enemyPrefab, GetRandomSpawnPos(), enemyInfo.enemyPrefab.transform.rotation);
                    aliveEnemies.Add(enemy);
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);
    }

    Vector3 GetRandomSpawnPos()
    {
        int[] validEvenNumbers = { -8, -6, -4, -2, 0, 2, 4, 6 };
        int x = validEvenNumbers[Random.Range(0, validEvenNumbers.Length)];
        return new Vector3(x, 0, 15);
    }
}
