using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Game
{
    public class ObstacleManager : MonoBehaviour
    {
        [SerializeField] private Obstacle _obstaclePrefab;

        [Space] [SerializeField] private ObstacleSpawnerConfigViewport _topSpawnerConfig;
        [SerializeField] private ObstacleSpawnerConfigViewport _leftSpawnerConfig;
        [SerializeField] private ObstacleSpawnerConfigViewport _rightSpawnerConfig;

        private ObstaclePooler _obstaclePooler;
        private ObstacleFactory _obstacleFactory;
        private IObstacleDespawner _obstacleDespawner;

        private Camera _viewportCamera;
        private float _viewportDepth;
        
        private IObstacleSpawner[] _spawners;

        private void Start()
        {
            // TODO: tie to gamecamera component
            _viewportCamera = Camera.main;
            _viewportDepth = 10;
            
            // TODO: move to bootstrap
            _obstaclePooler = new ObstaclePooler();
            _obstacleFactory = new ObstacleFactory(_obstaclePrefab.gameObject);
            _obstacleDespawner = new ObstacleDespawnerViewport(_viewportCamera);

            var data = CreateSpawnersData();
            CreateSpawners(data);
        }

        private void Update()
        {
            foreach (var spawner in _spawners)
                _obstacleDespawner.DespawnNecessaryObstacles(spawner.ActiveObstacles);
        }

        private ObstacleSpawnerDataViewport[] CreateSpawnersData()
        {
            var configs = new []
            {
                _topSpawnerConfig,
                _leftSpawnerConfig,
                _rightSpawnerConfig
            };

            var output = new ObstacleSpawnerDataViewport[3];

            for (int i = 0; i < 3; i++)
                output[i] = new ObstacleSpawnerDataViewport(
                    configs[i],
                    _obstaclePooler,
                    _obstacleFactory,
                    new ObstacleSpawnerDataCalculatorTop(configs[i], _viewportCamera, _viewportDepth)
                );
            
            return output;
        }

        private void CreateSpawners(ObstacleSpawnerDataViewport[] spawnersData)
        {
            _spawners = new IObstacleSpawner[spawnersData.Length];

            for (var i = 0; i < spawnersData.Length; i++)
            {
                var spawner = new ObstacleSpawnerViewport(spawnersData[i]);
                _spawners[i] = spawner;
                StartCoroutine(SpawnerRoutine(spawner));
            }
        }

        private IEnumerator SpawnerRoutine(IObstacleSpawnerViewport spawner)
        {
            while (spawner.ShouldSpawn)
            {
                spawner.SpawnAndInit();

                yield return new WaitForSeconds(spawner.SpawningInterval);
                spawner.RegisterSpawnedObstacles();
            }
        }
    }

    public class ObstacleDespawnerViewport : IObstacleDespawner
    {
        private readonly Camera _camera;

        public ObstacleDespawnerViewport(Camera viewportCamera)
        {
            _camera = viewportCamera;
        }

        public void DespawnNecessaryObstacles(IObstacle[] obstacles)
        {
            var count = obstacles.Length;

            for (var i = count - 1; i >= 0; i--)
                if (IsBelowScreen(obstacles[i].Position))
                    obstacles[i].Despawn();
        }

        private bool IsBelowScreen(Vector2 obstaclePosition) =>
            _camera.WorldToViewportPoint(obstaclePosition).y < 0;
    }

    public interface IObstacleDespawner
    {
        void DespawnNecessaryObstacles(IObstacle[] obstacles);
    }
}