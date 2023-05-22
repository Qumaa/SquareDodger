using Project.Game;
using UnityEngine;

namespace Project
{
    public class ObstacleSpawner : IObstacleSpawner
    {
        private ObstaclePooler _obstaclePooler;
        private ObstacleFactory _obstacleFactory;

        private readonly ObstacleSpawnerConfig _config;

        public int SpawnedObstacles { get; private set; }

        public ObstacleSpawner(ObstacleSpawnerConfig config)
        {
            _config = config;

            _obstaclePooler = new ObstaclePooler();
            _obstacleFactory = new ObstacleFactory();
        }

        //TODO: resolve positioning of newly created object
        public IObstacle Spawn(Vector3 position)
        {
            Obstacle spawned;

            if (!_obstaclePooler.TryPop(out spawned))
                spawned = _obstacleFactory.CreateNew();

            spawned.OnDespawned += HandleDespawned;
            
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