using UnityEngine;

namespace Project.Game
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