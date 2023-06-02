using UnityEngine;

namespace Project.Game
{
    public class CameraOffsetCalculatorViewport : ICameraOffsetCalculator
    {
        private readonly Camera _viewportCamera;
        private float _lastInput;
        private Vector2 _cachedOffset;

        public CameraOffsetCalculatorViewport(Camera viewportCamera)
        {
            _viewportCamera = viewportCamera;
            _lastInput = float.NaN;
        }

        public Vector2 CalculateOffset(float bottomOffsetUnits)
        {
            if (bottomOffsetUnits == _lastInput)
                return _cachedOffset;

            _lastInput = bottomOffsetUnits;

            Vector2 center = _viewportCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f));
            Vector2 bottom = _viewportCamera.ViewportToWorldPoint(new Vector3(0.5f, 0));

            var diff = center - bottom;

            _cachedOffset = diff - diff.normalized * bottomOffsetUnits;
            return _cachedOffset;
        }
    }
}