using Project.Game;

namespace Project.Architecture
{
    public struct PausedGameplayFactory : IFactory<IGameplay>
    {
        private IFactory<IPlayerBlendingShaderMaintainer> _shaderMaintainerFactory;
        private IFactory<IPlayerWithShader> _playerFactory;
        private IFactory<IGameCamera> _gameCameraFactory;
        private IFactory<IObstacleManagerViewport> _obstacleManagerFactory;
        private IFactory<IGameFinisher> _gameFinisherFactory;
        private IFactory<IParticleGameBackground> _gameBackgroundFactory;

        public PausedGameplayFactory(IFactory<IPlayerBlendingShaderMaintainer> shaderMaintainerFactory,
            IFactory<IPlayerWithShader> playerFactory,
            IFactory<IObstacleManagerViewport> obstacleManagerFactory, IFactory<IGameCamera> gameCameraFactory,
            IFactory<IGameFinisher> gameFinisherFactory, IFactory<IParticleGameBackground> gameBackgroundFactory)
        {
            _shaderMaintainerFactory = shaderMaintainerFactory;
            _playerFactory = playerFactory;
            _obstacleManagerFactory = obstacleManagerFactory;
            _gameCameraFactory = gameCameraFactory;
            _gameFinisherFactory = gameFinisherFactory;
            _gameBackgroundFactory = gameBackgroundFactory;
        }

        public IGameplay CreateNew()
        {
            var shaderMaintainer = CreateShaderMaintainer();
            var player = CreatePlayer(shaderMaintainer);
            var obstacleManager = CreateObstacleManager(player, shaderMaintainer);
            var gameCamera = CreateGameCamera(player);
            var gameFinisher = CreateGameFinisher(player, gameCamera.CameraController, shaderMaintainer.MaintainedShader);
            var background = CreateGameBackground();

            var game = new Gameplay(player, gameCamera, obstacleManager, gameFinisher, background);
            game.Pause();

            return game;
        }

        private IGameFinisher CreateGameFinisher(IPlayer player,
            ICameraController cameraController,
            IPlayerBlendingShader playerShader)
        {
            var finisher = _gameFinisherFactory.CreateNew() as IAnimatedGameFinisher;
            finisher!.Player = player;
            finisher!.CameraController = cameraController;
            finisher!.PlayerShader = playerShader;
            return finisher;
        }

        private IGameCamera CreateGameCamera(IPlayerWithShader player)
        {
            var gameCamera = _gameCameraFactory.CreateNew();
            gameCamera.Target = player.Transform;
            gameCamera.Reset();
            return gameCamera;
        }

        private IObstacleManagerViewport CreateObstacleManager(IPlayerWithShader player,
            IPlayerBlendingShaderMaintainer shaderMaintainer)
        {
            var obstacleManager = _obstacleManagerFactory.CreateNew();
            obstacleManager.ObstacleDespawner.PlayerTransform = player.Transform;
            obstacleManager.ObstacleDespawner.PlayerBlendingRadius = shaderMaintainer.MaintainedShader.BlendingRadius;
            player.ObstaclesSource = obstacleManager;
            return obstacleManager;
        }

        private IPlayerWithShader CreatePlayer(IPlayerBlendingShaderMaintainer shaderMaintainer)
        {
            var player = _playerFactory.CreateNew();
            player.ShaderMaintainer = shaderMaintainer;
            return player;
        }

        private IPlayerBlendingShaderMaintainer CreateShaderMaintainer()
        {
            var shaderMaintainer = _shaderMaintainerFactory.CreateNew();
            shaderMaintainer.MaintainedShader = new PlayerBlendingShader();
            return shaderMaintainer;
        }

        private IParticleGameBackground CreateGameBackground()
        {
            var background = _gameBackgroundFactory.CreateNew();
            return background;
        }
    }
}