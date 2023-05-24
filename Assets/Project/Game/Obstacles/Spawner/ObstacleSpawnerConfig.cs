using UnityEngine;

namespace Project
{
    public abstract class ObstacleSpawnerConfig : ScriptableObject
    {
        [Header("General settings")] 
        [SerializeField] private int _obstaclesToSpawn;
        [SerializeField] private int _spawnInterval;
        [SerializeField] private int _obstaclesSpeed;

        public int ObstaclesToSpawn => _obstaclesToSpawn;

        public float SpawnInterval => _spawnInterval;

        public float ObstaclesSpeed => _obstaclesSpeed;
    }
}