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

        [Header("Parameters")] 
        [SerializeField] private Vector2 _referenceViewportSize = new(5, 2);

        [SerializeField] private Vector2 _divisionSize = new(0.05f, 0.25f);
        [SerializeField] private Vector2 _bigDivisionSize = new(0.1f, 0.7f);
        [SerializeField] private float _currentScoreSize = 0.5f;
        [SerializeField] private float _highestScoreSize = 0.5f;
        [SerializeField] private float _divisionGap = 1;
        [SerializeField] private int _bigDivisionInterval = 5;
        [SerializeField] private float _valueBetweenDivisions = 1;
        [SerializeField] private float _scaleLineThickness = 0.1f;
        [SerializeField] private float _numbersHeight = 0.8f;

        [Header("Visuals")] 
        [SerializeField] private Sprite _scaleLineSprite;
        [SerializeField] private Sprite _divisionSprite;
        [SerializeField] private Sprite _currentScoreSprite;
        [SerializeField] private Sprite _highestScoreSprite;

        private Vector2 _viewportSize;
        private Vector2 _viewportHalfSize;
        private Vector2 _viewportScaleFactor;

        private float _scalePosition;
        private float _scaleClampedPosition;
        private float _scaleUpperBound => _scaleClampedPosition + _viewportHalfSize.x;
        private float _scaleLowerBound => _scaleClampedPosition - _viewportHalfSize.x;

        private float _divisionGapScaled;
        
        private float _highestScore;
        private float _highestScorePosition;

        private Pooler<Division> _divisionsPooler;
        private List<Division> _visibleDivisions;
        private RectTransform _highestScoreIcon;
        private RectTransform _currentScoreIcon;

        #endregion

        #region Initialization

        protected override void Start()
        {
            base.Start();
            CacheViewportSize();
            InitializeScaleLine();
            InitializeScaleElements();
        }

        private void InitializeScaleLine()
        {
            var height = ReferenceHeightToViewportHeight(_scaleLineThickness);
            var line = _scaleLine.rectTransform;
            line.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            _scaleLine.sprite = _scaleLineSprite;
        }

        private void InitializeScaleElements()
        {
            _divisionsPooler = new Pooler<Division>();
            _visibleDivisions = new List<Division>();
            _highestScoreIcon = CreateScaleLineIcon(_highestScoreSprite);
            SetRectTransformSize(_highestScoreIcon, ReferenceHeightToViewportHeight(_highestScoreSize));
            _currentScoreIcon = CreateScaleLineIcon(_currentScoreSprite);
            SetRectTransformSize(_currentScoreIcon, ReferenceHeightToViewportHeight(_currentScoreSize));
        }
        
        private RectTransform CreateScaleLineIcon(Sprite icon)
        {
            var obj = new GameObject();

            var trans = obj.AddComponent<RectTransform>();
            trans.SetParent(_scaleLine.transform, false);

            var img = obj.AddComponent<Image>();
            img.sprite = icon;

            return trans;
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
        
        public void SetHighestScore(float score)
        {
            _highestScore = score;
            UpdateHighestScorePosition();
        }

        private void ClearDisplay()
        {
            foreach (var div in _visibleDivisions)
            {
                _divisionsPooler.Push(div);
                div.SetActive(false);
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
            // TODO
        }

        private void DrawHighestScore()
        {
            if (!IsVisibleOnScaleLine(_highestScorePosition, _highestScoreSize))
            {
                _highestScoreIcon.gameObject.SetActive(false);
                return;
            }
            
            _highestScoreIcon.gameObject.SetActive(true);
            PositionTransformOnScaleLine(_highestScoreIcon, _highestScorePosition);
        }

        private void DrawCurrentScore()
        {
            PositionTransformOnScaleLine(_currentScoreIcon, _scalePosition);
        }

        private void UpdateHighestScorePosition() =>
            _highestScorePosition = _highestScore / _valueBetweenDivisions;

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
            PositionTransformOnScaleLine(division.RectTransform, position);
        }
        
        private Vector2 GetDivisionSizeAt(float position)
        {
            var big = ShouldBeBigDivisionAt(position / _valueBetweenDivisions);
            return big ? _bigDivisionSize : _divisionSize;
        }

        private bool ShouldBeBigDivisionAt(float position) =>
            Mathf.RoundToInt(Mathf.Round(position / _divisionGapScaled) % _bigDivisionInterval) == 0;

        private Division GetDivision(bool big)
        {
            var div = _divisionsPooler.CanPop() ? _divisionsPooler.Pop() : CreateNewDivision();
            div.IsBig = big;
            div.SetActive(true);
            _visibleDivisions.Add(div);
            return div;
        }

        private void PositionTransformOnScaleLine(Transform transformToPlace, float scalePosition)
        {
            var normalizedPos = (scalePosition - _scaleLowerBound) / (_scaleUpperBound - _scaleLowerBound);
            var xPos = (normalizedPos - 0.5f) * _scaleLine.rectTransform.rect.width;
            transformToPlace.localPosition = new Vector3(xPos, 0);
        }

        private Division CreateNewDivision()
        {
            var trans = CreateScaleLineIcon(_divisionSprite);
            UpdateScaleHierarchyOrder();
            return new Division(this, trans);
        }

        private void UpdateScaleHierarchyOrder()
        {
            _highestScoreIcon.SetAsLastSibling();
            _currentScoreIcon.SetAsLastSibling();
        }

        #endregion

        #region Utils

        private float ReferenceHeightToViewportHeight(float referenceUnits) =>
            _viewport.rect.height / (_referenceViewportSize.y / referenceUnits);

        private float ReferenceWidthToViewportWidth(float referenceUnits) =>
            _viewport.rect.width / (_referenceViewportSize.x / referenceUnits);
        
        private Vector2 ReferenceSizeToViewportSize(Vector2 referenceSize) =>
        new(ReferenceWidthToViewportWidth(referenceSize.x), ReferenceHeightToViewportHeight(referenceSize.y));
        
        private static void SetRectTransformSize(RectTransform rectTransform, Vector2 size)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        }
        
        private static void SetRectTransformSize(RectTransform rectTransform, float size) =>
            SetRectTransformSize(rectTransform, new Vector2(size, size));

        private bool IsVisibleOnScaleLine(float position, float width) =>
            _viewportHalfSize.x - width / 2 <= Mathf.Abs(_scaleClampedPosition - position);

        #endregion
        
        private class Division
        {
            private readonly ScaleScoreDisplay _parent;
            private bool _isBig;
            private Vector2 _size;

            public bool IsBig
            {
                get => _isBig;
                set => SetBig(value);
            }

            public Vector2 Size
            {
                get => _size;
                private set => SetSize(value);
            }

            public RectTransform RectTransform { get; }

            public Division(ScaleScoreDisplay parent, RectTransform rectTransform)
            {
                _parent = parent;
                RectTransform = rectTransform;
            }

            public void SetActive(bool active) =>
            RectTransform.gameObject.SetActive(active);

            private void SetBig(bool big)
            {
                _isBig = big;
                Size = _isBig ? _parent._bigDivisionSize : _parent._divisionSize;
            }

            private void SetSize(Vector2 size)
            {
                _size = size;
                SetRectTransformSize(RectTransform, _parent.ReferenceSizeToViewportSize(_size));
            }
        }
    }
}