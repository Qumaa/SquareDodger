using Project.Game;

namespace Project.Architecture
{
    public struct PlayerBlendingShaderMaintainerFactory : IFactory<IBlendingShaderMaintainer>
    {
        private IDisposer _disposer;
        private IFactory<IBlendingShader> _shaderFactory;
        private readonly float _blendingRadius;
        private readonly float _blendingLength;

        public PlayerBlendingShaderMaintainerFactory(IDisposer disposer, IFactory<IBlendingShader> shaderFactory,
            float blendingRadius, float blendingLength)
        {
            _disposer = disposer;
            _shaderFactory = shaderFactory;
            _blendingRadius = blendingRadius;
            _blendingLength = blendingLength;
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