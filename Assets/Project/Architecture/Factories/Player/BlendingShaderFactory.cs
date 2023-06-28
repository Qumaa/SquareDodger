using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct BlendingShaderFactory : IFactory<IBlendingShader>
    {
        private float _blendingRadius;
        private float _blendingLength;
        private Material _material;

        public BlendingShaderFactory(float blendingRadius, float blendingLength, Material material)
        {
            _blendingRadius = blendingRadius;
            _blendingLength = blendingLength;
            _material = material;
        }

        public IBlendingShader CreateNew()
        {
            var shader = new PlayerBlendingShader(_material)
            {
                HardBlendingRadius = _blendingRadius,
                SoftBlendingLength = _blendingLength,
            };
            
            return shader;
        }
    }
}