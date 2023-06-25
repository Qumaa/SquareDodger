using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.UI
{
    public class ScaleScoreDisplay : UIBehaviour, IGameScoreDisplay
    {
    #region Values

        [Header("Elements")] 
        [SerializeField] private RectTransform _viewport;
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
        [SerializeField] private float _numbersFontSize = 40;
        [SerializeField] private float _numbersOffset = 0.1f;

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
        private Pooler<TextMeshProUGUI> _numberLabelsPooler;
        private List<TextMeshProUGUI> _visibleNumberLabels;
        private RectTransform _highestScoreIcon;
        private RectTransform _currentScoreIcon;
        private Image _currentScoreImage;
        private Vector3[] _viewportCornersTable;

    #endregion

    #region Initialization

        protected override void Awake()
        {
            base.Awake();
            InitializeFieldValues();
        }

        protected override void Start()
        {
            base.Start();
            CacheViewportSize();
            InitializeScaleLine();
            InitializeScaleElements();
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            CacheViewportSize();
        }

        private void InitializeFieldValues()
        {
            _viewportCornersTable = new Vector3[4];
            _divisionsPooler = new Pooler<Division>();
            _visibleDivisions = new List<Division>();
            _numberLabelsPooler = new Pooler<TextMeshProUGUI>();
            _visibleNumberLabels = new List<TextMeshProUGUI>();
            _highestScoreIcon = CreateScaleLineIcon(_highestScoreSprite);
            _currentScoreIcon = CreateScaleLineIcon(_currentScoreSprite);
            _currentScoreImage = _currentScoreIcon.GetComponent<Image>();
        }

        private void CacheViewportSize()
        {
            if (ViewportNotActive())
                return;
            
            _viewport.GetWorldCorners(_viewportCornersTable);

            var rotatedSize = _viewportCornersTable[2] - _viewportCornersTable[0];
            var unrotatedSize = (Vector2) (Quaternion.Inverse(_viewport.rotation) * rotatedSize);

            _viewportSize = unrotatedSize;
            
            UpdateViewportDependantValues();
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
            SetRectTransformSize(_highestScoreIcon, ReferenceHeightToViewportHeight(_highestScoreSize));
            SetRectTransformSize(_currentScoreIcon, ReferenceHeightToViewportHeight(_currentScoreSize));
        }

        private RectTransform CreateScaleLineIcon(Sprite icon)
        {
            var trans = CreateScaleLineObject();

            var img = trans.gameObject.AddComponent<Image>();
            img.sprite = icon;

            return trans;
        }

        private RectTransform CreateScaleLineObject()
        {
            var obj = new GameObject();

            var trans = obj.AddComponent<RectTransform>();
            trans.SetParent(_scaleLine.transform, false);
            
            return trans;
        }

        private void UpdateViewportDependantValues()
        {
            _viewportScaleFactor = _viewportSize / _referenceViewportSize;
            _viewportHalfSize = _viewportSize / 2f;
            
            _divisionGapScaled = _divisionGap * _viewportScaleFactor.x;
            
            UpdateHighestScorePosition();
        }

    #endregion

    #region Display

        public void DisplayScore(float score)
        {
            SetScoreAsPosition(score);
            Repaint();
        }

        public void SetHighestScore(float score)
        {
            _highestScore = score;
            UpdateHighestScorePosition();
        }

        private void SetScoreAsPosition(float score)
        {
            var position = ScoreToScaleLinePosition(score);

            _scalePosition = position;
            _scaleClampedPosition = Mathf.Max(position, _viewportHalfSize.x);
        }

        private void Repaint()
        {
            if (ViewportNotActive())
                return;
            
            ClearDisplay();
            DrawScaleDivisions();
            DrawHighestScore();
            DrawCurrentScore();
            DrawScaleNumbers();
        }

        private void UpdateHighestScorePosition() =>
            _highestScorePosition = ScoreToScaleLinePosition(_highestScore);

        private float ScoreToScaleLinePosition(float score) =>
            score / _valueBetweenDivisions * _viewportScaleFactor.x;

        private void ClearDisplay()
        {
            foreach (var div in _visibleDivisions)
            {
                _divisionsPooler.Push(div);
                div.SetActive(false);
            }

            _visibleDivisions.Clear();

            foreach (var label in _visibleNumberLabels)
            {
                _numberLabelsPooler.Push(label);
                label.gameObject.SetActive(false);
            }
            
            _visibleNumberLabels.Clear();
        }

        private void DrawScaleDivisions()
        {
            var divisionsCount = CalculateDivisionsCountForCurrentPosition();
            var firstPos = CalculateFirstDivisionPositionFromLeft();

            for (var i = 0; i < divisionsCount; i++)
                DisplayDivision(firstPos + i * _divisionGapScaled);
        }

        private void DrawHighestScore()
        {
            if (!IsVisibleOnScaleLine(_highestScorePosition, _highestScoreSize) || CurrentScoreHigherThanHighestScore())
            {
                _highestScoreIcon.gameObject.SetActive(false);
                return;
            }
            
            _highestScoreIcon.gameObject.SetActive(true);
            PositionTransformOnScaleLine(_highestScoreIcon, _highestScorePosition);
        }

        private void DrawCurrentScore()
        {
            _currentScoreImage.sprite =
                CurrentScoreHigherThanHighestScore() ? _highestScoreSprite : _currentScoreSprite;
            
            PositionTransformOnScaleLine(_currentScoreIcon, _scalePosition);
        }

        private void DrawScaleNumbers()
        {
            DrawNumbersUnderDivisions();
            DrawNumberUnderHighestScore();
            DrawNumberUnderCurrentScore();
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
            var width = GetDivisionSizeAt(_scaleLowerBound).x;
            return CalculateFirstDivisionWithInterval(width);
        }

        private float CalculateFirstDivisionWithInterval(float width, float interval = 1)
        {
            interval *= _divisionGapScaled;
            return Mathf.Ceil((_scaleLowerBound - width / 2) / interval) * interval;
        }

        private void DisplayDivision(float position)
        {
            var big = ShouldBeBigDivisionAt(position);

            var division = GetDivision(big);
            PositionTransformOnScaleLine(division.RectTransform, position);
        }

        private bool IsVisibleOnScaleLine(float position, float width) =>
            _viewportHalfSize.x + width / 2 > Mathf.Abs(_scaleClampedPosition - position);

        private bool CurrentScoreHigherThanHighestScore() =>
            _highestScorePosition <= _scalePosition;

        private void PositionTransformOnScaleLine(Transform transformToPlace, float scalePosition, float verticalOffset = 0)
        {
            var normalizedPos = (scalePosition - _scaleLowerBound) / (_scaleUpperBound - _scaleLowerBound);
            var xPos = (normalizedPos - 0.5f) * _scaleLine.rectTransform.rect.width;
            var yPos = ReferenceHeightToViewportHeight(verticalOffset);
            transformToPlace.localPosition = new Vector3(xPos, yPos);
        }

        private void DrawNumbersUnderDivisions()
        {
            var firstPos = CalculateFirstBigDivisionPositionFromLeft();
            var previous = GetBigDivisionRelativelyTo(firstPos, -1);

            var pos = previous;
            var verticalOffset = -(_bigDivisionSize.y / 2 + _numbersOffset);
            Vector2 textSize;
            TextMeshProUGUI text;
            
            do
            {
                text = GetNumberLabel();
                textSize = CalculateTextWidthAtPosition(text, pos);
                if (IsVisibleOnScaleLine(pos, textSize.x))
                    DrawNumberAtPosition(text, pos, verticalOffset - textSize.y / 2);

                pos = GetBigDivisionRelativelyTo(pos, 1);
            } while (IsVisibleOnScaleLine(pos, textSize.x));
        }

        private TextMeshProUGUI GetNumberLabel()
        {
            var label = _numberLabelsPooler.CanPop() ? _numberLabelsPooler.Pop() : CreateNewNumberLabel();
            _visibleNumberLabels.Add(label);
            label.gameObject.SetActive(true);
            return label;
        }

        private TextMeshProUGUI CreateNewNumberLabel()
        {
            var trans = CreateScaleLineObject();
            var text = trans.gameObject.AddComponent<TextMeshProUGUI>();
            // TODO: update when viewport changes
            text.fontSize = _numbersFontSize * _viewportScaleFactor.x;
            return text;
        }

        private void DrawNumberUnderHighestScore()
        {
        }

        private void DrawNumberUnderCurrentScore()
        {
        }

        private Vector2 CalculateTextWidthAtPosition(TextMeshProUGUI text, float pos) =>
            text.GetPreferredValues(NumberToString(pos)) / _viewport.rect.size * _viewportSize;

        private float GetBigDivisionRelativelyTo(float pos, int offset) =>
            pos + _divisionGapScaled * _bigDivisionInterval * offset;

        private float CalculateFirstBigDivisionPositionFromLeft() =>
            CalculateFirstDivisionWithInterval(_bigDivisionSize.x, _bigDivisionInterval);

        private void DrawNumberAtPosition(TextMeshProUGUI text, float position, float verticalOffset)
        {
            var str = NumberToString(position / _viewportScaleFactor.x * _valueBetweenDivisions);
            text.text = str;
            text.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, text.GetPreferredValues(str).x);
            PositionTransformOnScaleLine(text.transform, position, verticalOffset);
        }

        private string NumberToString(float number) =>
            number.ToString("F1");

        private Vector2 GetDivisionSizeAt(float position)
        {
            var big = ShouldBeBigDivisionAt(position / _valueBetweenDivisions);
            return big ? _bigDivisionSize : _divisionSize;
        }

        private bool ShouldBeBigDivisionAt(float position) =>
            Mathf.RoundToInt(position / _divisionGapScaled) % _bigDivisionInterval == 0;

        private Division GetDivision(bool big)
        {
            var div = _divisionsPooler.CanPop() ? _divisionsPooler.Pop() : CreateNewDivision();
            div.IsBig = big;
            div.SetActive(true);
            _visibleDivisions.Add(div);
            return div;
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

        private bool ViewportNotActive() =>
            !_viewport.gameObject.activeInHierarchy;

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