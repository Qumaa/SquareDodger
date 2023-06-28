using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct BlendingShaderMaintainerFactory : IFactory<IBlendingShaderMaintainer>
    {
        private IDisposer _disposer;
        private IFactory<IBlendingShader> _shaderFactory;
        private readonly float _blendingRadius;
        private readonly float _blendingLength;

        public BlendingShaderMaintainerFactory(IDisposer disposer,
            float blendingRadius, float blendingLength, Material material)
        {
            _disposer = disposer;
            _blendingRadius = blendingRadius;
            _blendingLength = blendingLength;
            _shaderFactory = new BlendingShaderFactory(_blendingRadius, _blendingLength, material);
        }

        public IBlendingShaderMaintainer CreateNew()
        {
            var shaderMaintainer = new PlayerBlendingShaderMaintainer(_blendingRadius, _blendingLength);
            shaderMaintainer.MaintainedShader = _shaderFactory.CreateNew();
            _disposer.Register(shaderMaintainer);

            return shaderMaintainer;
        }
    }
}