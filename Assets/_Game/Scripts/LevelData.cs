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
    public int count;
}
