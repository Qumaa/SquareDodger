using System.Collections.Generic;

namespace Project.Game
{
    public class ObstacleSpawnerViewport : IObstacleSpawner
    {
        private List<IObstacle> _activeObstacles;
        
        private ObstacleViewportSpawnerConfig _config;
        private IPooler<IObstacle> _pooler;
        private IFactory<IObstacle> _factory;
        private IObstacleSpawnerDataCalculator _calculator;

        public int SpawnedObstacles { get; private set; }
        public bool ShouldSpawn => SpawnedObstacles < _config.ObstaclesToSpawn;
        public float SpawningInterval => _config.SpawnInterval;
        public IObstacle[] ActiveObstacles { get; private set; }
        public IObstacleColorSource ColorSource { get; set; }

        public ObstacleSpawnerViewport(ObstacleViewportSpawnerConfig config, IPooler<IObstacle> pooler,
            IFactory<IObstacle> factory, IObstacleSpawnerDataCalculator calculator)
        {
            _config = config;
            _pooler = pooler;
            _factory = factory;
            _calculator = calculator;
            _activeObstacles = new List<IObstacle>(_config.ObstaclesToSpawn);
            UpdateActiveObstaclesProperty();
        }

        public void SpawnAndInit()
        {
            var spawned = SpawnObstacle();
            InitObstacle(spawned);
        }

        private IObstacle SpawnObstacle()
        {
            if (!_pooler.TryPop(out var spawned))
                spawned = _factory.CreateNew();

            SpawnedObstacles++;
            _activeObstacles.Add(spawned);
            UpdateActiveObstaclesProperty();
            return spawned;
        }

        private void InitObstacle(IObstacle spawned)
        {
            spawned.Init();
            spawned.Color = ColorSource.ObstacleColor;
            spawned.OnDespawned += HandleDespawned;
            spawned.Position = _calculator.CalculatePosition();
            spawned.Velocity = _calculator.CalculateVelocity();
        }

        private void HandleDespawned(IObstacle despawned)
        {
            despawned.OnDespawned -= HandleDespawned;
            
            _activeObstacles.Remove(despawned);
            UpdateActiveObstaclesProperty();
            
            SpawnedObstacles--;
        }

        private void UpdateActiveObstaclesProperty() =>
            ActiveObstacles = _activeObstacles.ToArray();
    }
}