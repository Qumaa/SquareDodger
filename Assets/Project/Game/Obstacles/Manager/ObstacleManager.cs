using System.Collections;
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
            _obstacleDespawner = new ObstacleDespawnerViewport(_viewportCamera, new Vector2(0.707f, 0.707f) * _obstaclePrefab.Size);

            var data = CreateSpawnersData();
            CreateSpawners(data);
        }

        private void Update()
        {
            for (int i = 0; i < _spawners.Length; i++)
            {
                var spawner = _spawners[i];
                _obstacleDespawner.DespawnNecessaryObstacles(spawner.ActiveObstacles);
            }
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
}