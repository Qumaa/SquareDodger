using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Game
{
    public class ObstacleDespawnerViewportShader : IObstacleDespawnerViewportShader
    {
        private readonly Camera _camera;
        private Vector2 _positionOffset;
        private readonly ObstaclePooler _obstaclePooler;

        public Transform PlayerTransform { get; set; }
        public float PlayerBlendingRadius { get; set; }

        public event Action<IObstacle> OnDespawned;

        public ObstacleDespawnerViewportShader(Camera viewportCamera, Vector2 positionOffset,
            ObstaclePooler obstaclePooler)
        {
            _camera = viewportCamera;
            _positionOffset = positionOffset;
            _obstaclePooler = obstaclePooler;
        }

        public void DespawnNecessaryObstacles(List<IObstacle> obstacles)
        {
            if (obstacles == null)
                return;
            
            var count = obstacles.Count;

            for (int i = count - 1; i >= 0; i--)
            {
                var obstacle = obstacles[i];

                if (ShouldDespawn(obstacle))
                    DespawnSingle(obstacle);
            }
        }

        private bool ShouldDespawn(IObstacle obstacle) =>
            IsBelowScreen(obstacle) &&
            DoesntBlendPlayer(obstacle);

        private void DespawnSingle(IObstacle obstacle)
        {
            _obstaclePooler.Push(obstacle);
            obstacle.Despawn();
            OnDespawned?.Invoke(obstacle);
        }

        private bool IsBelowScreen(IObstacle obstacle) =>
            _camera.WorldToViewportPoint(obstacle.Position + _positionOffset).y < 0;

        private bool DoesntBlendPlayer(IObstacle obstacle) =>
            Vector2.Distance(obstacle.Position, PlayerTransform.position) > PlayerBlendingRadius;
    }
}