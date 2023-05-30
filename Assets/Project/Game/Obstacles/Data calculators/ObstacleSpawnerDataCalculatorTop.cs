using UnityEngine;

namespace Project.Game
{
    class ObstacleSpawnerDataCalculatorTop : ObstacleSpawnerDataCalculatorViewport
    {
        private readonly Vector2[] _dirs;

        private int _current;

        public ObstacleSpawnerDataCalculatorTop
        (ObstacleSpawnerConfigViewport config, Camera viewportCamera, float depth) : 
            base(config, viewportCamera, depth)
        {
            _dirs = new[] {Vector2.left, Vector2.down};
            _current = 0;
        }

        protected override Vector2 CalculateVelocity(float speed)
        {
            var vec = _dirs[_current];
            _current = (int) Mathf.Repeat(_current + 1, _dirs.Length);
            return vec * speed;
        }
    }
}