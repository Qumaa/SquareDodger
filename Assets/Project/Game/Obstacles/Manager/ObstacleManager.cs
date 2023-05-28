using System.Collections;
using UnityEngine;

namespace Project.Game
{
    public class ObstacleManager : IObstacleManager
    {
        private IObstacleSpawner[] _spawners;
        private SpawnerInfo[] _spawnerInfos;
        private IObstacleDespawner _despawner;

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