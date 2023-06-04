using Project.Game;

namespace Project.Architecture
{
    public struct PausedGameplayFactory : IFactory<IGameplay>
    {
        private IFactory<IPlayerShaderMaintainer> _shaderMaintainerFactory;
        private IFactory<IPlayerWithShader> _playerFactory;
        private IFactory<IGameCamera> _gameCameraFactory;
        private IFactory<IObstacleManagerViewport> _obstacleManagerFactory;
        private IFactory<IGameFinisher> _gameFinisherFactory;

        public PausedGameplayFactory(IFactory<IPlayerShaderMaintainer> shaderMaintainerFactory,
            IFactory<IPlayerWithShader> playerFactory,
            IFactory<IObstacleManagerViewport> obstacleManagerFactory, IFactory<IGameCamera> gameCameraFactory,
            IFactory<IGameFinisher> gameFinisherFactory)
        {
            _shaderMaintainerFactory = shaderMaintainerFactory;
            _playerFactory = playerFactory;
            _obstacleManagerFactory = obstacleManagerFactory;
            _gameCameraFactory = gameCameraFactory;
            _gameFinisherFactory = gameFinisherFactory;
        }

        public IGameplay CreateNew()
        {
            var shaderMaintainer = CreateShaderMaintainer();
            var player = CreatePlayer(shaderMaintainer);
            var obstacleManager = CreateObstacleManager(player, shaderMaintainer);
            var gameCamera = CreateGameCamera(player);
            var gameFinisher = CreateGameFinisher();

            var game = new Gameplay(player, gameCamera, obstacleManager, gameFinisher);
            game.Pause();

            return game;
        }

        private IGameFinisher CreateGameFinisher()
        {
            return _gameFinisherFactory.CreateNew();
        }

        private IGameCamera CreateGameCamera(IPlayerWithShader player)
        {
            var gameCamera = _gameCameraFactory.CreateNew();
            gameCamera.Target = player.Transform;
            gameCamera.Reset();
            return gameCamera;
        }

        private IObstacleManagerViewport CreateObstacleManager(IPlayerWithShader player,
            IPlayerShaderMaintainer shaderMaintainer)
        {
            var obstacleManager = _obstacleManagerFactory.CreateNew();
            obstacleManager.ObstacleDespawner.PlayerTransform = player.Transform;
            obstacleManager.ObstacleDespawner.PlayerBlendingRadius = shaderMaintainer.BlendingRadius;
            player.ObstaclesSource = obstacleManager;
            return obstacleManager;
        }

        private IPlayerWithShader CreatePlayer(IPlayerShaderMaintainer shaderMaintainer)
        {
            var player = _playerFactory.CreateNew();
            player.ShaderMaintainer = shaderMaintainer;
            return player;
        }

        private IPlayerShaderMaintainer CreateShaderMaintainer()
        {
            return _shaderMaintainerFactory.CreateNew();
        }
    }
}