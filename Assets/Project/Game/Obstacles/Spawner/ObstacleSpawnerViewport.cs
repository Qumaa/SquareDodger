using System;

namespace Project.Game
{
    public class ObstacleSpawnerViewport : IObstacleSpawner
    {
        private ObstacleViewportSpawnerRuntimeData _config;
        private IPooler<IObstacle> _pooler;
        private IFactory<IObstacle> _factory;
        private IObstacleSpawnerDataCalculator _calculator;
        private int _spawnedObstacles;

        public bool ShouldSpawn => _spawnedObstacles < _config.ObstaclesToSpawn;
        public float SpawningInterval => _config.SpawnInterval;

        public event Action<IObstacle> OnSpawned;
        public event Action<IObstacle> OnCreated;

        public ObstacleSpawnerViewport(ObstacleViewportSpawnerRuntimeData config, IPooler<IObstacle> pooler,
            IFactory<IObstacle> factory, IObstacleSpawnerDataCalculator calculator)
        {
            _config = config;
            _pooler = pooler;
            _factory = factory;
            _calculator = calculator;
        }

        public void SpawnAndInit()
        {
            var spawned = SpawnObstacle();
            InitObstacle(spawned);
            OnSpawned?.Invoke(spawned);
        }

        private IObstacle SpawnObstacle()
        {
            if (!_pooler.TryPop(out var spawned))
            {
                spawned = _factory.CreateNew();
                OnCreated?.Invoke(spawned);
            }

            _spawnedObstacles++;
            return spawned;
        }

        private void InitObstacle(IObstacle spawned)
        {
            spawned.Init();
            spawned.OnDespawned += HandleDespawned;
            spawned.Position = _calculator.CalculatePosition();
            spawned.Velocity = _calculator.CalculateVelocity();
        }

        private void HandleDespawned(IObstacle despawned)
        {
            despawned.OnDespawned -= HandleDespawned;
            
            _spawnedObstacles--;
        }
    }
}