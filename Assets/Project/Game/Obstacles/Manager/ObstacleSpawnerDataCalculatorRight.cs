using UnityEngine;

namespace Project
{
    class ObstacleSpawnerDataCalculatorRight : ObstacleSpawnerDataCalculatorViewport
    {
        public ObstacleSpawnerDataCalculatorRight(ObstacleSpawnerConfigViewport config, Camera viewportCamera, float depth) : base(config, viewportCamera, depth)
        {
        }

        protected override Vector2 CalculateVelocity(float speed) =>
            Vector2.left * speed;
    }
}