namespace Project.Game
{
    public class ObstacleManager : IObstacleManager
    {
        private IObstacleSpawner[] _spawners;
        private SpawnerInfo[] _spawnerInfos;
        private IObstacleDespawner _despawner;

        public IObstacle[] ActiveObstacles => GetActiveObstacles();

        public ObstacleManager(IObstacleSpawner[] spawners, IObstacleDespawner despawner)
        {
            _spawners = spawners;
            _despawner = despawner;

            _spawnerInfos = new SpawnerInfo[_spawners.Length];
            for (var i = 0; i < _spawnerInfos.Length; i++)
                _spawnerInfos[i] = new SpawnerInfo(_spawners[i]);
        }

        public void Update(float timeStep)
        {
            for (var i = 0; i < _spawners.Length; i++)
            {
                var spawner = _spawners[i];
                var info = _spawnerInfos[i];
                
                _despawner.DespawnNecessaryObstacles(spawner.ActiveObstacles);
                UpdateSpawner(info, timeStep);
            }
        }

        private void UpdateSpawner(SpawnerInfo info, float timeStep)
        {
            if (info.Counter > 0)
            {
                info.Counter -= timeStep;
                return;
            }

            var spawner = info.Spawner;

            if (spawner.ShouldSpawn)
            {
                spawner.SpawnAndInit();
                
                info.Counter += spawner.SpawningInterval;
            }
        }

        // this is called every frame. DO NOT use linq, foreach or whatever else that allocates
        private IObstacle[] GetActiveObstacles()
        {
            var length = 0;

            for (var i = 0; i < _spawners.Length; i++)
                length += _spawners[i].ActiveObstacles.Length;

            var obstacles = new IObstacle[length];

            var counter = 0;
            for (var i = 0; i < _spawners.Length; i++)
            {
                for (var y = 0; y < _spawners[i].ActiveObstacles.Length; y++)
                {
                    obstacles[counter++] = _spawners[i].ActiveObstacles[y];
                }
            }

            return obstacles;
        }

        private class SpawnerInfo
        {
            public IObstacleSpawner Spawner;
            public float Counter;

            public SpawnerInfo(IObstacleSpawner spawner)
            {
                Spawner = spawner;
                Counter = 0;
            }
        }
    }
}