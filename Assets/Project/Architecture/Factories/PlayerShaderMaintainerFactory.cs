using Project.Game;

namespace Project.Architecture
{
    public struct PlayerShaderMaintainerFactory : IFactory<IPlayerBlendingShaderMaintainer>
    {
        private IDisposer _disposer;

        public PlayerShaderMaintainerFactory(IDisposer disposer)
        {
            _disposer = disposer;
        }

        public IPlayerBlendingShaderMaintainer CreateNew()
        {
            var shaderMaintainer = new PlayerBlendingShaderMaintainer();
            _disposer.Register(shaderMaintainer);

            return shaderMaintainer;
        }
    }
}