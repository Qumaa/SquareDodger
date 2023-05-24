using System.Collections;
using UnityEngine;

namespace Project
{
    public class ObstacleManager : MonoBehaviour
    {
        [SerializeField] private Obstacle _obstaclePrefab;

        [Space] [SerializeField] private ObstacleSpawnerConfigViewport _topSpawnerConfig;
        [SerializeField] private ObstacleSpawnerConfigViewport _leftSpawnerConfig;
        [SerializeField] private ObstacleSpawnerConfigViewport _rightSpawnerConfig;

        private ObstaclePooler _obstaclePooler;
        private ObstacleFactory _obstacleFactory;

        private Camera _viewportCamera;
        private float _viewportDepth;

        private void Start()
        {
            _obstaclePooler = new ObstaclePooler();
            _obstacleFactory = new ObstacleFactory(_obstaclePrefab.gameObject);

            _viewportCamera = Camera.main;
            _viewportDepth = 10;
            
            var datas = CreateSpawnerDatas();
            CreateSpawners(datas);
        }

        private ObstacleSpawnerDataViewport[] CreateSpawnerDatas()
        {
            var top = new ObstacleSpawnerDataViewport(
                _topSpawnerConfig,
                _obstaclePooler,
                _obstacleFactory,
                new ObstacleSpawnerDataCalculatorTop(_topSpawnerConfig, _viewportCamera, _viewportDepth)
            );

            var left = new ObstacleSpawnerDataViewport(
                _leftSpawnerConfig,
                _obstaclePooler,
                _obstacleFactory,
                new ObstacleSpawnerDataCalculatorLeft(_leftSpawnerConfig, _viewportCamera, _viewportDepth)
            );

            var right = new ObstacleSpawnerDataViewport(
                _rightSpawnerConfig,
                _obstaclePooler,
                _obstacleFactory,
                new ObstacleSpawnerDataCalculatorRight(_rightSpawnerConfig, _viewportCamera, _viewportDepth)
            );

            return new[] {top, left, right};
        }

        private void CreateSpawners(ObstacleSpawnerDataViewport[] spawnerDatas)
        {
            foreach (var data in spawnerDatas)
            {
                var spawner = new ObstacleSpawnerViewport(data);
                StartCoroutine(SpawnerRoutine(spawner));
            }
        }

        private IEnumerator SpawnerRoutine(IObstacleSpawnerViewport spawner)
        {
            while (spawner.ShouldSpawn)
            {
                var pos = spawner.Data.Calculator.CalculatePosition();
                spawner.Spawn().SetVelocity(spawner.Data.Calculator.CalculateVelocity());

                yield return new WaitForSeconds(spawner.SpawningInterval);
            }
        }
    }
}