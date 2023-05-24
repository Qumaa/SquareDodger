using Project.Game;

namespace Project
{
    public class ObstacleSpawnerViewport : IObstacleSpawnerViewport
    {
        private readonly ObstacleSpawnerDataViewport _data;

        public int SpawnedObstacles { get; private set; }
        private ObstacleSpawnerConfigViewport _config => _data.Config;
        public bool ShouldSpawn => SpawnedObstacles < _config.ObstaclesToSpawn;
        public float SpawningInterval => _config.SpawnInterval;
        public float ObstacleSpeed => _config.ObstaclesSpeed;

        public ObstacleSpawnerDataViewport Data => _data;

        public ObstacleSpawnerViewport(ObstacleSpawnerDataViewport data)
        {
            _data = data;
        }

        public IObstacle Spawn()
        {
            Obstacle spawned;

            if (!_data.Pooler.TryPop(out spawned))
                spawned = _data.Factory.CreateNew();

            spawned.OnDespawned += HandleDespawned;

            var position = _data.Calculator.CalculatePosition();
            spawned.transform.position = position;

            SpawnedObstacles++;
            return spawned;
        }

        private void HandleDespawned(IObstacle obstacle)
        {
            obstacle.OnDespawned -= HandleDespawned;
            SpawnedObstacles--;
        }
    }
}