using UnityEngine;

namespace Project.Game
{
    public class PlayerBlendingShader : IBlendingShader
    {
        private const string _MATERIAL_BLENDING_RADIUS_NAME = "_BlendingRadius";
        private const string _MATERIAL_BLENDING_WIDTH_NAME = "_BlendingLength";
        private const string _MATERIAL_COLOR_BALANCE_PROPERTY_NAME = "_ColorBalance";
        private const string _PLAYER_COLOR_PROPERTY_NAME = "_PlayerColor";
        private const string _BLENDING_COLOR_PROPERTY_NAME = "_ObstacleColor";
        private const string _BLENDING_MODE_PROPERTY_NAME = "_ColorBlend";
        private const string _BLENDING_MODE_OFF = "_COLORBLEND_OFF";
        private const string _BLENDING_MODE_VERT = "_COLORBLEND_VERT";
        private const string _BLENDING_MODE_FRAG = "_COLORBLEND_FRAG";
        private static readonly int _blendingRadiusNameId = Shader.PropertyToID(_MATERIAL_BLENDING_RADIUS_NAME);
        private static readonly int _blendingLengthNameId = Shader.PropertyToID(_MATERIAL_BLENDING_WIDTH_NAME);
        private static readonly int _colorBalanceNameId = Shader.PropertyToID(_MATERIAL_COLOR_BALANCE_PROPERTY_NAME);
        private static readonly int _playerColorPropertyId = Shader.PropertyToID(_PLAYER_COLOR_PROPERTY_NAME);
        private static readonly int _blendingColorPropertyId = Shader.PropertyToID(_BLENDING_COLOR_PROPERTY_NAME);
        private static readonly int _blendingModePropertyId = Shader.PropertyToID(_BLENDING_MODE_PROPERTY_NAME);

        private float _blendingRadius;
        private float _blendingLength;
        private float _colorBalance;
        private Color32 _playerColor;
        private Color32 _blendingColor;
        private ShaderBlendingMode _blendingMode;
        private int _blendingModeCachedAsInt;
        private readonly string[] _blendingModesTable = {
            _BLENDING_MODE_OFF,
            _BLENDING_MODE_VERT,
            _BLENDING_MODE_FRAG
        };

        public Material Material { get; }

        public Color32 PlayerColor
        {
            get => _playerColor;
            set => SetPlayerColor(value);
        }

        public Color32 BlendingColor
        {
            get => _blendingColor;
            set => SetBlendingColor(value);
        }

        public float ColorBalance
        {
            get => _colorBalance;
            set => SetColorBalance(value);
        }

        public float HardBlendingRadius
        {
            get => _blendingRadius;
            set => SetBlendingRadius(value);
        }

        public float SoftBlendingLength
        {
            get => _blendingLength;
            set => SetBlendingLength(value);
        }

        public float TotalBlendingRadius => HardBlendingRadius + SoftBlendingLength;

        public ShaderBlendingMode BlendingMode
        {
            get => _blendingMode;
            set => SetBlendingMode(value);
        }

        public PlayerBlendingShader(Material material)
        {
            Material = material;

            _blendingModeCachedAsInt = (int) Material.GetFloat("_ColorBlend");
            _blendingMode = (ShaderBlendingMode) _blendingModeCachedAsInt;
        }

        private void SetPlayerColor(Color32 color)
        {
            _playerColor = color;
            UpdatePlayerColor();
        }

        private void SetBlendingColor(Color32 color)
        {
            _blendingColor = color;
            UpdateBlendingColor();
        }

        private void SetColorBalance(float balance)
        {
            balance = Mathf.Clamp01(balance);
            _colorBalance = balance;
            UpdateColorBalance();
        }

        private void SetBlendingRadius(float radius)
        {
            _blendingRadius = radius;
            UpdateBlendingRadius();
        }

        private void SetBlendingLength(float length)
        {
            _blendingLength = length;
            UpdateBlendingLength();
        }

        private void SetBlendingMode(ShaderBlendingMode mode)
        {
            _blendingMode = mode;
            UpdateBlendingMode();
        }

        private void UpdateBlendingColor() =>
            Material.SetColor(_blendingColorPropertyId, _blendingColor);

        private void UpdatePlayerColor() =>
            Material.SetColor(_playerColorPropertyId, _playerColor);

        private void UpdateColorBalance() =>
            Material.SetFloat(_colorBalanceNameId, _colorBalance);

        private void UpdateBlendingLength() =>
            Material.SetFloat(_blendingLengthNameId, _blendingLength);

        private void UpdateBlendingRadius() =>
            Material.SetFloat(_blendingRadiusNameId, _blendingRadius);

        private void UpdateBlendingMode()
        {
            Material.DisableKeyword(_blendingModesTable[_blendingModeCachedAsInt]);
            _blendingModeCachedAsInt = (int) _blendingMode;
            Material.SetFloat(_blendingModePropertyId, _blendingModeCachedAsInt);
            Material.EnableKeyword(_blendingModesTable[_blendingModeCachedAsInt]);
        }
    }
}