using UnityEngine;

namespace Project.Game
{
    class ObstacleSpawnerDataCalculatorRight : ObstacleSpawnerDataCalculatorViewport
    {
        public ObstacleSpawnerDataCalculatorRight(ObstacleViewportSpawnerConfig config, Camera viewportCamera, float depth) : base(config, viewportCamera, depth)
        {
        }

        protected override Vector2 CalculateVelocity(float speed) =>
            Vector2.left * speed;
    }
}