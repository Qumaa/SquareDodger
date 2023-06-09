using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct ObstacleManagerSpawnerFactory : IFactory<IObstacleSpawner[]>
    {
        private ObstacleViewportSpawnerConfig _topSpawnerConfig;
        private ObstacleViewportSpawnerConfig _leftSpawnerConfig;
        private ObstacleViewportSpawnerConfig _rightSpawnerConfig;
        private Camera _controlledCamera;
        private float _cameraViewportDepth;
        private IPooler<IObstacle> _obstaclePooler;
        private IFactory<IObstacle> _obstacleFactory;

        public ObstacleManagerSpawnerFactory(ObstacleManagerRuntimeData managerConfig, Camera controlledCamera, 
            float cameraViewportDepth, IPooler<IObstacle> obstaclePooler, IFactory<IObstacle> obstacleFactory)
        {
            _topSpawnerConfig = managerConfig.TopSpawnerConfig;
            _leftSpawnerConfig = managerConfig.LeftSpawnerConfig;
            _rightSpawnerConfig = managerConfig.RightSpawnerConfig;
            
            _controlledCamera = controlledCamera;
            _cameraViewportDepth = cameraViewportDepth;
            _obstaclePooler = obstaclePooler;
            _obstacleFactory = obstacleFactory;
        }

        public IObstacleSpawner[] CreateNew()
        {
            var spawnersData = CreateSpawnersData();
            var spawners = new IObstacleSpawner[spawnersData.Length];

            for (var i = 0; i < spawnersData.Length; i++)
            {
                var spawner = new ObstacleSpawnerViewport(
                    spawnersData[i].Config,
                    spawnersData[i].Pooler,
                    spawnersData[i].Factory,
                    spawnersData[i].Calculator
                );
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
                new ObstacleSpawnerDataCalculatorTop(_topSpawnerConfig, _controlledCamera, _cameraViewportDepth),
                new ObstacleSpawnerDataCalculatorLeft(_leftSpawnerConfig, _controlledCamera, _cameraViewportDepth),
                new ObstacleSpawnerDataCalculatorRight(_rightSpawnerConfig, _controlledCamera, _cameraViewportDepth)
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
        
        private struct ObstacleSpawnerDataViewport
        {
            public readonly ObstacleViewportSpawnerConfig Config;
            public readonly IPooler<IObstacle> Pooler;
            public readonly IFactory<IObstacle> Factory;
            public readonly IObstacleSpawnerDataCalculator Calculator;

            public ObstacleSpawnerDataViewport(ObstacleViewportSpawnerConfig config, IPooler<IObstacle> pooler,
                IFactory<IObstacle> factory, IObstacleSpawnerDataCalculator calculator)
            {
                Config = config;
                Pooler = pooler;
                Factory = factory;
                Calculator = calculator;
            }
        }
    }
}