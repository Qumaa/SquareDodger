using UnityEngine;

namespace Project
{
    class ObstacleSpawnerDataCalculatorLeft : ObstacleSpawnerDataCalculatorViewport
    {
        public ObstacleSpawnerDataCalculatorLeft(ObstacleSpawnerConfigViewport config, Camera viewportCamera, float depth) : base(config, viewportCamera, depth)
        {
        }

        protected override Vector2 CalculateVelocity(float speed) =>
            Vector2.down * speed;
    }
}