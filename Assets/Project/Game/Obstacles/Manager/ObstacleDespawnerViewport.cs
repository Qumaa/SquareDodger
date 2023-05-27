using UnityEngine;

namespace Project.Game
{
    public class ObstacleDespawnerViewport : IObstacleDespawner
    {
        private readonly Camera _camera;
        public Vector2 PositionOffset { get; set; }

        public ObstacleDespawnerViewport(Camera viewportCamera, Vector2 positionOffset)
        {
            _camera = viewportCamera;
            PositionOffset = positionOffset;
        }
        
        public ObstacleDespawnerViewport(Camera viewportCamera)
        {
            _camera = viewportCamera;
        }

        public void DespawnNecessaryObstacles(IObstacle[] obstacles)
        {
            var count = obstacles.Length;

            for (var i = count - 1; i >= 0; i--)
                if (IsBelowScreen(obstacles[i]))
                    obstacles[i].Despawn();
        }

        private bool IsBelowScreen(IObstacle obstacle) =>
            _camera.WorldToViewportPoint(obstacle.Position + PositionOffset).y < 0;
    }
}