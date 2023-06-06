using UnityEngine;

namespace Project.Architecture
{
    public struct ViewportBackgroundSizeCalculator : IGameBackgroundSizeCalculator
    {
        private Vector2 _viewportSize;

        public ViewportBackgroundSizeCalculator(Camera viewportCamera, Vector2 extraSize)
        {
            var height = viewportCamera.orthographicSize * 2;
            var width = height * viewportCamera.aspect;
            
            _viewportSize = new Vector2(width, height) + extraSize;
        }

        public Vector2 Calculate()
        {
            return _viewportSize;
        }
    }
}