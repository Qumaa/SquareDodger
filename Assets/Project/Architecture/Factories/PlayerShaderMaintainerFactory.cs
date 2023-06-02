using Project.Game;

namespace Project.Architecture
{
    public struct PlayerShaderMaintainerFactory : IFactory<IPlayerShaderMaintainer>
    {
        private IDisposer _disposer;

        public PlayerShaderMaintainerFactory(IDisposer disposer)
        {
            _disposer = disposer;
        }

        public IPlayerShaderMaintainer CreateNew()
        {
            var shaderMaintainer = new PlayerShaderMaintainer();
            _disposer.Register(shaderMaintainer);

            return shaderMaintainer;
        }
    }
}