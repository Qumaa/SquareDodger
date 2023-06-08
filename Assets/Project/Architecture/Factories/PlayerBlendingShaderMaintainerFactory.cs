using Project.Game;

namespace Project.Architecture
{
    public struct PlayerBlendingShaderMaintainerFactory : IFactory<IPlayerBlendingShaderMaintainer>
    {
        private IDisposer _disposer;
        private IFactory<IPlayerBlendingShader> _shaderFactory;
        private readonly float _blendingRadius;
        private readonly float _blendingLength;

        public PlayerBlendingShaderMaintainerFactory(IDisposer disposer, IFactory<IPlayerBlendingShader> shaderFactory,
            float blendingRadius, float blendingLength)
        {
            _disposer = disposer;
            _shaderFactory = shaderFactory;
            _blendingRadius = blendingRadius;
            _blendingLength = blendingLength;
        }

        public IPlayerBlendingShaderMaintainer CreateNew()
        {
            var shaderMaintainer = new PlayerBlendingShaderMaintainer(_blendingRadius, _blendingLength);
            shaderMaintainer.MaintainedShader = _shaderFactory.CreateNew();
            _disposer.Register(shaderMaintainer);

            return shaderMaintainer;
        }
    }
}