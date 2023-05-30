using System.Collections.Generic;

namespace Project.Game
{
    public class ObstacleSpawnerViewport : IObstacleSpawnerViewport
    {
        private readonly ObstacleSpawnerDataViewport _data;
        private List<IObstacle> _activeObstacles;

        public int SpawnedObstacles { get; private set; }
        public bool ShouldSpawn => SpawnedObstacles < _data.Config.ObstaclesToSpawn;
        public float SpawningInterval => _data.Config.SpawnInterval;
        public IObstacle[] ActiveObstacles { get; private set; }

        public ObstacleSpawnerDataViewport Data => _data;

        public ObstacleSpawnerViewport(ObstacleSpawnerDataViewport data)
        {
            _data = data;
            _activeObstacles = new List<IObstacle>(_data.Config.ObstaclesToSpawn);
            UpdateActiveObstaclesProperty();
        }

        public void SpawnAndInit()
        {
            IObstacle spawned;

            if (!_data.Pooler.TryPop(out spawned))
                spawned = _data.Factory.CreateNew();

            spawned.OnDespawned += HandleDespawned;
            spawned.Position = _data.Calculator.CalculatePosition();
            spawned.Velocity = _data.Calculator.CalculateVelocity();

            SpawnedObstacles++;
            _activeObstacles.Add(spawned);
            UpdateActiveObstaclesProperty();
        }

        private void HandleDespawned(IObstacle despawned)
        {
            despawned.OnDespawned -= HandleDespawned;
            
            _data.Pooler.Push(despawned);
            _activeObstacles.Remove(despawned);
            UpdateActiveObstaclesProperty();
            
            SpawnedObstacles = --SpawnedObstacles;
        }

        private void UpdateActiveObstaclesProperty() =>
            ActiveObstacles = _activeObstacles.ToArray();
    }
}