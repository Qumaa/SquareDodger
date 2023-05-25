﻿using System.Collections.Generic;

namespace Project.Game
{
    public class ObstacleSpawnerViewport : IObstacleSpawnerViewport
    {
        private readonly ObstacleSpawnerDataViewport _data;
        private int _spawnedObstaclesInternal;
        private List<IObstacle> _activeObstacles;

        public int SpawnedObstacles { get; private set; }
        private ObstacleSpawnerConfigViewport _config => _data.Config;
        public bool ShouldSpawn => SpawnedObstacles < _config.ObstaclesToSpawn;
        public float SpawningInterval => _config.SpawnInterval;
        public IObstacle[] ActiveObstacles => _activeObstacles.ToArray();
        public float ObstacleSpeed => _config.ObstaclesSpeed;

        public ObstacleSpawnerDataViewport Data => _data;

        public ObstacleSpawnerViewport(ObstacleSpawnerDataViewport data)
        {
            _data = data;
            _spawnedObstaclesInternal = 0;
            _activeObstacles = new List<IObstacle>(_data.Config.ObstaclesToSpawn);
        }

        public void SpawnAndInit()
        {
            IObstacle spawned;

            if (!_data.Pooler.TryPop(out spawned))
                spawned = _data.Factory.CreateNew();

            spawned.OnDespawned += HandleDespawned;
            spawned.Position = _data.Calculator.CalculatePosition();
            spawned.Velocity = _data.Calculator.CalculateVelocity();

            _spawnedObstaclesInternal++;
            _activeObstacles.Add(spawned);
        }

        public void RegisterSpawnedObstacles() =>
            SpawnedObstacles = _spawnedObstaclesInternal;

        private void HandleDespawned(IObstacle despawned)
        {
            despawned.OnDespawned -= HandleDespawned;
            
            _data.Pooler.Push(despawned);
            _activeObstacles.Remove(despawned);
            
            _spawnedObstaclesInternal = --SpawnedObstacles;
        }
    }
}