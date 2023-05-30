using UnityEngine;

namespace Project.Game
{
    public class ObstacleDespawnerViewportShader : IObstacleDespawner
    {
        private readonly Camera _camera;
        private Vector2 _positionOffset;
        private IPlayer _player;

        public ObstacleDespawnerViewportShader(Camera viewportCamera, Vector2 positionOffset, IPlayer player)
        {
            _camera = viewportCamera;
            _positionOffset = positionOffset;
            _player = player;
        }

        public void DespawnNecessaryObstacles(IObstacle[] obstacles)
        {
            if (obstacles == null)
                return;
            
            var count = obstacles.Length;

            for (int i = count - 1; i >= 0; i--)
                if (ShouldDespawn(obstacles[i]))
                    obstacles[i].Despawn();
        }

        private bool ShouldDespawn(IObstacle obstacle) =>
            IsBelowScreen(obstacle) &&
            DoesntBlendPlayer(obstacle, _player);

        private bool IsBelowScreen(IObstacle obstacle) =>
            _camera.WorldToViewportPoint(obstacle.Position + _positionOffset).y < 0;

        private bool DoesntBlendPlayer(IObstacle obstacle, IPlayer player) =>
            Vector2.Distance(obstacle.Position, player.Transform.position) > player.ShaderMaintainer.BlendingRadius;
    }
}