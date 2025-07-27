using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public List<Wave> waves;
    public float timeBetweenWaves = 3f;
}

[System.Serializable]
public class Wave
{
    public List<EnemySpawnInfo> enemies;
}

[System.Serializable]
public class EnemySpawnInfo
{
    public GameObject enemyPrefab;
    [Range(0, 1)] public float spawnChance; // 0.1 = %10
    public int count;
}
