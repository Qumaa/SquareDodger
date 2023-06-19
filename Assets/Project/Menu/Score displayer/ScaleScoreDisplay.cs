using System;
using UnityEngine;

namespace Project.UI
{
    public class ScaleScoreDisplay : MonoBehaviour
    {
        #region Values

        [SerializeField] private RectTransform _viewport;
        private Vector2 _viewportSize;
        
        private float _scaleRelativePosition;
        private float _scaleUpperBound => _scaleRelativePosition + _viewportSize.x;
        private float _scaleLowerBound => _scaleRelativePosition - _viewportSize.x;
        
        #endregion

        #region Initialization

        private void Start()
        {
            _viewportSize = CalculateViewportSize();
            Debug.Log(_viewportSize);
        }

        private Vector2 CalculateViewportSize()
        {
            Vector3[] results = new Vector3[4];
            _viewport.GetWorldCorners(results);

            var rotatedSize = results[2] - results[0];
            var unrotatedSize = Quaternion.Inverse(_viewport.rotation) * rotatedSize;

            return unrotatedSize;
        }

        #endregion

        #region Display

        public void DisplayScore(float score)
        {
            SetScaleRelativePosition(score);
            DrawScaleDivisions();
            DrawNumbers();
            DrawHighestScore();
        }

        private void SetScaleRelativePosition(float position)
        {
            throw new NotImplementedException();
        }

        private void DrawScaleDivisions()
        {
            throw new NotImplementedException();
        }

        private void DrawNumbers()
        {
            throw new NotImplementedException();
        }

        private void DrawHighestScore()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
