using System.Collections.Generic;
using UnityEngine;

namespace Project.Game
{
    public class ObstacleManagerViewport : PausableAndResettable, IObstacleManagerViewport
    {
        private IObstacleSpawner[] _spawners;
        private SpawnerInfo[] _spawnerInfos;
        private List<IObstacle> _allObstacles;
        private Color32 _obstaclesColor;

        public List<IObstacle> ActiveObstacles { get; }

        public IObstacleDespawnerViewportShader ObstacleDespawner { get; }

        public ObstacleManagerViewport(IObstacleSpawner[] spawners, IObstacleDespawnerViewportShader despawner)
        {
            _spawners = spawners;
            ObstacleDespawner = despawner;
            ObstacleDespawner.OnDespawned += HandleDespawned;

            ActiveObstacles = new List<IObstacle>();
            _allObstacles = new List<IObstacle>();
            InitializeSpawners();
        }

        public void Update(float timeStep)
        {
            if (_isPaused)
                return;

            foreach (var info in _spawnerInfos)
                UpdateSpawner(info, timeStep);

            ObstacleDespawner.DespawnNecessaryObstacles(ActiveObstacles);
        }

        public void ApplyTheme(IGameTheme theme)
        {
            _obstaclesColor = theme.ObstacleColor;
            
            foreach (var obstacle in _allObstacles)
                obstacle.Color = _obstaclesColor;
        }

        protected override void OnPaused()
        {
            base.OnPaused();
            foreach (var obstacle in ActiveObstacles)
                obstacle.Pause();
        }

        protected override void OnResumed()
        {
            base.OnResumed();
            foreach (var obstacle in ActiveObstacles)
                obstacle.Resume();
        }

        protected override void OnReset()
        {
            base.OnReset();
            for (var i = ActiveObstacles.Count - 1; i >= 0; i--)
                ObstacleDespawner.DespawnSingle(ActiveObstacles[i]);
        }

        private void InitializeSpawners()
        {
            _spawnerInfos = new SpawnerInfo[_spawners.Length];
            for (var i = 0; i < _spawnerInfos.Length; i++)
            {
                _spawnerInfos[i] = new SpawnerInfo(_spawners[i]);

                _spawners[i].OnSpawned += HandleSpawned;
                _spawners[i].OnCreated += HandleCreated;
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

            if (!spawner.ShouldSpawn)
                return;

            spawner.SpawnAndInit();
            info.Counter += spawner.SpawningInterval;
        }

        private void HandleSpawned(IObstacle spawned) =>
            ActiveObstacles.Add(spawned);

        private void HandleDespawned(IObstacle despawned) =>
            ActiveObstacles.Remove(despawned);

        private void HandleCreated(IObstacle created)
        {
            _allObstacles.Add(created);
            created.Color = _obstaclesColor;
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