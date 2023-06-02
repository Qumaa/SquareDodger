using Project.Game;

namespace Project.Architecture
{
    public struct PausedGameplayFactory : IFactory<IGameplay>
    {
        private IFactory<IPlayerShaderMaintainer> _shaderMaintainerFactory;
        private IFactory<IPlayerWithShader> _playerFactory;
        private IGameCamera _gameCamera;
        private IFactory<IObstacleManagerViewport> _obstacleManagerFactory;

        public PausedGameplayFactory(IFactory<IPlayerShaderMaintainer> shaderMaintainerFactory,
            IFactory<IPlayerWithShader> playerFactory,
            IFactory<IObstacleManagerViewport> obstacleManagerFactory, IGameCamera gameCamera)
        {
            _shaderMaintainerFactory = shaderMaintainerFactory;
            _playerFactory = playerFactory;
            _obstacleManagerFactory = obstacleManagerFactory;
            _gameCamera = gameCamera;
        }

        public IGameplay CreateNew()
        {
            var shaderMaintainer = _shaderMaintainerFactory.CreateNew();

            var player = _playerFactory.CreateNew();
            player.ShaderMaintainer = shaderMaintainer;
            
            var obstacleManager = _obstacleManagerFactory.CreateNew();
            obstacleManager.ObstacleDespawner.Player = player.Transform;
            obstacleManager.ObstacleDespawner.PlayerBlendingRadius = shaderMaintainer.BlendingRadius;
            
            _gameCamera.Target = player.Transform;
            _gameCamera.Reset();
            
            var game = new Gameplay(player, shaderMaintainer, _gameCamera, obstacleManager);
            game.Pause();

            return game;
        }
    }
}