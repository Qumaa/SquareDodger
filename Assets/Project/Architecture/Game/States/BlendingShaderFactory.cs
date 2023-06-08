using UnityEngine;

namespace Project.Architecture
{
    public struct BlendingShaderFactory : IFactory<IPlayerBlendingShader>
    {
        private float _blendingRadius;
        private float _blendingLength;
        private Color32 _playerColor;
        private Color32 _blendingColor;

        public BlendingShaderFactory(float blendingRadius, float blendingLength, Color32 playerColor, Color32 blendingColor)
        {
            _blendingRadius = blendingRadius;
            _blendingLength = blendingLength;
            _playerColor = playerColor;
            _blendingColor = blendingColor;
        }

        public IPlayerBlendingShader CreateNew()
        {
            var shader = new PlayerBlendingShader
            {
                BlendingRadius = _blendingRadius,
                BlendingLength = _blendingLength,
                PlayerColor = _playerColor,
                BlendingColor = _blendingColor
            };
            
            return shader;
        }
    }
}