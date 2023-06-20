using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.UI
{
    public class ScaleScoreDisplay : UIBehaviour
    {
        #region Values

        [Header("Elements")] [SerializeField] private RectTransform _viewport;
        [SerializeField] private Image _scaleLine;

        [Header("Parameters")] [SerializeField]
        private Vector2 _referenceViewportSize = new(5, 2);

        [SerializeField] private Vector2 _divisionSize = new(0.05f, 0.25f);
        [SerializeField] private Vector2 _bigDivisionSize = new(0.1f, 0.7f);
        [SerializeField] private float _divisionGap = 1;
        [SerializeField] private int _bigDivisionInterval = 5;
        [SerializeField] private float _valueBetweenDivisions = 1;
        [SerializeField] private float _scaleLineThickness = 0.1f;
        [SerializeField] private float _numbersHeight = 0.8f;

        [Header("Visuals")] [SerializeField] private Sprite _scaleLineSprite;
        [SerializeField] private Sprite _divisionSprite;

        private Vector2 _viewportSize;
        private Vector2 _viewportHalfSize;
        private Vector2 _viewportScaleFactor;

        private float _scalePosition;
        private float _scaleClampedPosition;
        private float _scaleUpperBound => _scaleClampedPosition + _viewportHalfSize.x;
        private float _scaleLowerBound => _scaleClampedPosition - _viewportHalfSize.x;

        private float _divisionGapScaled;

        private Pooler<RectTransform> _divisionsPooler;
        private List<RectTransform> _visibleDivisions;

        #endregion

        #region Initialization

        protected override void Start()
        {
            base.Start();
            CacheViewportSize();

            var height = ReferenceHeightToViewportHeight(_scaleLineThickness);
            var line = _scaleLine.rectTransform;
            line.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            _scaleLine.sprite = _scaleLineSprite;

            _divisionsPooler = new Pooler<RectTransform>();
            _visibleDivisions = new List<RectTransform>();
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            CacheViewportSize();
            UpdateViewportDependantValues();
        }

        private void CacheViewportSize()
        {
            var results = new Vector3[4];
            _viewport.GetWorldCorners(results);

            var rotatedSize = results[2] - results[0];
            var unrotatedSize = (Vector2) (Quaternion.Inverse(_viewport.rotation) * rotatedSize);

            _viewportSize = unrotatedSize;
            _viewportHalfSize = _viewportSize / 2f;
            _viewportScaleFactor = _viewportSize / _referenceViewportSize;
        }

        private void UpdateViewportDependantValues()
        {
            _divisionGapScaled = _divisionGap * _viewportScaleFactor.x;
        }

        #endregion

        #region Display

        public void DisplayScore(float score)
        {
            ClearDisplay();
            SetScoreAsPosition(score);
            DrawScaleDivisions();
            DrawNumbers();
            DrawHighestScore();
            DrawCurrentScore();
        }

        private void ClearDisplay()
        {
            foreach (var div in _visibleDivisions)
            {
                _divisionsPooler.Push(div);
                div.gameObject.SetActive(false);
            }

            _visibleDivisions.Clear();
        }

        private void SetScoreAsPosition(float score)
        {
            var position = score / _valueBetweenDivisions;

            _scalePosition = position;
            _scaleClampedPosition = Mathf.Max(position, _viewportHalfSize.x);
        }

        private void DrawScaleDivisions()
        {
            var divisionsCount = CalculateDivisionsCountForCurrentPosition();
            var firstPos = CalculateFirstDivisionPositionFromLeft();

            for (var i = 0; i < divisionsCount; i++)
                DisplayDivision(firstPos + i * _divisionGapScaled);
        }

        private void DrawNumbers()
        {
            throw new NotImplementedException();
        }

        private void DrawHighestScore()
        {
            throw new NotImplementedException();
        }

        private void DrawCurrentScore()
        {
            throw new NotImplementedException();
        }

        private int CalculateDivisionsCountForCurrentPosition()
        {
            var lowerDivSize = GetDivisionSizeAt(_scaleLowerBound).x;
            var upperDivSize = GetDivisionSizeAt(_scaleUpperBound).x;

            var lowerBound = Mathf.FloorToInt(_scaleUpperBound / _divisionGapScaled + 1 + upperDivSize);
            var upperBound = Mathf.CeilToInt(_scaleLowerBound / _divisionGapScaled - 1 - lowerDivSize);
            return lowerBound - upperBound - 1;
        }

        private float CalculateFirstDivisionPositionFromLeft()
        {
            var offset = GetDivisionSizeAt(_scaleLowerBound).x;
            return Mathf.Ceil((_scaleLowerBound - offset / 2) / _divisionGapScaled) * _divisionGapScaled;
        }

        private void DisplayDivision(float position)
        {
            var big = ShouldBeBigDivisionAt(position);

            var division = GetDivision(big);
            PositionTransformOnScaleLine(division, position);
        }

        private RectTransform GetDivision(bool big)
        {
            var div = _divisionsPooler.CanPop() ? _divisionsPooler.Pop() : CreateNewDivision();

            var size = big ? _bigDivisionSize : _divisionSize;
            div.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ReferenceWidthToViewportWidth(size.x));
            div.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ReferenceHeightToViewportHeight(size.y));

            div.gameObject.SetActive(true);
            _visibleDivisions.Add(div);
            return div;
        }

        private RectTransform CreateNewDivision()
        {
            var div = new GameObject();

            var trans = div.AddComponent<RectTransform>();
            trans.SetParent(_scaleLine.transform, false);

            var img = div.AddComponent<Image>();
            img.sprite = _divisionSprite;

            return trans;
        }

        private Vector2 GetDivisionSizeAt(float position)
        {
            var big = ShouldBeBigDivisionAt(position / _valueBetweenDivisions);
            return big ? _bigDivisionSize : _divisionSize;
        }

        private bool ShouldBeBigDivisionAt(float position) =>
            Mathf.RoundToInt(Mathf.Round(position / _divisionGapScaled) % _bigDivisionInterval) == 0;

        private void PositionTransformOnScaleLine(Transform transformToPlace, float scalePosition)
        {
            var normalizedPos = (scalePosition - _scaleLowerBound) / (_scaleUpperBound - _scaleLowerBound);
            var xPos = (normalizedPos - 0.5f) * _scaleLine.rectTransform.rect.width;
            transformToPlace.localPosition = new Vector3(xPos, 0);
        }

        #endregion

        #region Utils

        private float ReferenceHeightToViewportHeight(float referenceUnits) =>
            _viewport.rect.height / (_referenceViewportSize.y / referenceUnits);

        private float ReferenceWidthToViewportWidth(float referenceUnits) =>
            _viewport.rect.width / (_referenceViewportSize.x / referenceUnits);

        #endregion
    }
}