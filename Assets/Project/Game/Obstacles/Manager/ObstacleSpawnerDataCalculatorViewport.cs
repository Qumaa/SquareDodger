using UnityEngine;

namespace Project.Game
{
    public abstract class ObstacleSpawnerDataCalculatorViewport : IObstacleSpawnerDataCalculator
    {
        private readonly ObstacleSpawnerConfigViewport _config;
        private readonly float _depth;
        private readonly Camera _viewportCamera;

        private Vector2 _viewportFrom => _config.ViewportFrom;
        private Vector2 _viewportTo => _config.ViewportTo;
        private Vector3 _offset => _config.Offset;

        protected ObstacleSpawnerDataCalculatorViewport(
            ObstacleSpawnerConfigViewport config,
            Camera viewportCamera,
            float depth)
        {
            _config = config;
            _depth = depth;
            _viewportCamera = viewportCamera;
        }

        public Vector3 CalculatePosition()
        {
            var viewportSpacePosition = Vector2.Lerp(_viewportFrom, _viewportTo, Random.value);
            var inWorldSpace =
                _viewportCamera.ViewportToWorldPoint(new Vector3(viewportSpacePosition.x, viewportSpacePosition.y, _depth));
            var withOffset = inWorldSpace + _offset;

            return withOffset;
        }

        protected abstract Vector2 CalculateVelocity(float speed);
        public Vector2 CalculateVelocity() => CalculateVelocity(_config.ObstaclesSpeed);
    }
}