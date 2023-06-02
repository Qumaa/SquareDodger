using UnityEngine;

namespace Project.Game
{
    public class ObstacleManagerViewport : PausableAndResettable, IObstacleManagerViewport
    {
        private IObstacleSpawner[] _spawners;
        private IObstacleDespawnerViewportShader _despawner;
        private SpawnerInfo[] _spawnerInfos;

        public IObstacle[] ActiveObstacles => GetActiveObstacles();

        public IObstacleDespawnerViewportShader ObstacleDespawner => _despawner;

        public ObstacleManagerViewport(IObstacleSpawner[] spawners, IObstacleDespawnerViewportShader despawner)
        {
            _spawners = spawners;
            _despawner = despawner;

            CreateSpawnersInfo();
        }

        public void Update(float timeStep)
        {
            if (_isPaused)
                return;
            
            for (var i = 0; i < _spawners.Length; i++)
            {
                var spawner = _spawners[i];
                var info = _spawnerInfos[i];
                
                _despawner.DespawnNecessaryObstacles(spawner.ActiveObstacles);
                UpdateSpawner(info, timeStep);
            }
            
        }

        protected override void OnPaused()
        {
            base.OnPaused();
            foreach(var obstacle in ActiveObstacles)
                obstacle.Pause();
        }

        protected override void OnResumed()
        {
            base.OnResumed();
            foreach(var obstacle in ActiveObstacles)
                obstacle.Resume();
        }

        protected override void OnReset()
        {
            base.OnReset();
            foreach(var obstacle in ActiveObstacles)
                obstacle.Despawn();
        }

        private void CreateSpawnersInfo()
        {
            _spawnerInfos = new SpawnerInfo[_spawners.Length];
            for (var i = 0; i < _spawnerInfos.Length; i++)
                _spawnerInfos[i] = new SpawnerInfo(_spawners[i]);
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
            public readonly IObstacleSpawner Spawner;
            public float Counter;

            public SpawnerInfo(IObstacleSpawner spawner)
            {
                Spawner = spawner;
                Counter = 0;
            }
        }
    }
}