using UnityEngine;

namespace Project.Game
{
    public abstract class ObstacleSpawnerConfig : ScriptableObject
    {
        [Header("General settings")] 
        [SerializeField] private int _obstaclesToSpawn;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _obstaclesSpeed;

        public int ObstaclesToSpawn => _obstaclesToSpawn;

        public float SpawnInterval => _spawnInterval;

        public float ObstaclesSpeed => _obstaclesSpeed;
    }
}