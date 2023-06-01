namespace Project.Game
{
    public struct ObstacleManagerFactory : IFactory<IObstacleManager>
    {
        private ObstacleManagerConfig _managerConfig;
        private IGameCamera _gameCamera;
        private IPooler<IObstacle> _obstaclePooler;
        private IFactory<IObstacle> _obstacleFactory;
        private IObstacleDespawner _obstacleDespawner;

        private ObstacleSpawnerConfigViewport _topSpawnerConfig => _managerConfig.TopSpawnerConfig;
        private ObstacleSpawnerConfigViewport _leftSpawnerConfig => _managerConfig.LeftSpawnerConfig;
        private ObstacleSpawnerConfigViewport _rightSpawnerConfig => _managerConfig.RightSpawnerConfig;

        public ObstacleManagerFactory(ObstacleManagerConfig managerConfig, IGameCamera gameCamera,
            IPooler<IObstacle> obstaclePooler, IFactory<IObstacle> obstacleFactory,
            IObstacleDespawner obstacleDespawner)
        {
            _managerConfig = managerConfig;
            _gameCamera = gameCamera;
            _obstaclePooler = obstaclePooler;
            _obstacleFactory = obstacleFactory;
            _obstacleDespawner = obstacleDespawner;
        }

        public IObstacleManager CreateNew()
        {
            return new ObstacleManager(CreateSpawners(), _obstacleDespawner);
        }

        private IObstacleSpawner[] CreateSpawners()
        {
            var spawnersData = CreateSpawnersData();
            var spawners = new IObstacleSpawner[spawnersData.Length];

            for (var i = 0; i < spawnersData.Length; i++)
            {
                var spawner = new ObstacleSpawnerViewport(spawnersData[i]);
                spawners[i] = spawner;
            }

            return spawners;
        }

        private ObstacleSpawnerDataViewport[] CreateSpawnersData()
        {
            var configs = new []
            {
                _topSpawnerConfig,
                _leftSpawnerConfig,
                _rightSpawnerConfig
            };

            var calculators = new ObstacleSpawnerDataCalculatorViewport[]
            {
                new ObstacleSpawnerDataCalculatorTop(_topSpawnerConfig, _gameCamera.ControlledCamera, _gameCamera.ViewportDepth),
                new ObstacleSpawnerDataCalculatorLeft(_leftSpawnerConfig, _gameCamera.ControlledCamera, _gameCamera.ViewportDepth),
                new ObstacleSpawnerDataCalculatorRight(_rightSpawnerConfig, _gameCamera.ControlledCamera, _gameCamera.ViewportDepth)
            };

            const int len = 3;
            var output = new ObstacleSpawnerDataViewport[len];

            for (var i = 0; i < len; i++)
                output[i] = new ObstacleSpawnerDataViewport(
                    configs[i],
                    _obstaclePooler,
                    _obstacleFactory,
                    calculators[i]
                );
            
            return output;
        }
    }
}