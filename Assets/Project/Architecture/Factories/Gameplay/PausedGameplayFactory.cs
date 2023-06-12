using Project.Game;

namespace Project.Architecture
{
    public struct PausedGameplayFactory : IFactory<IGameplay>
    {
        private readonly IGameThemeAppenderComposite _themeApplier;
        private IFactory<IPlayerBlendingShaderMaintainer> _shaderMaintainerFactory;
        private IFactory<IPlayerWithShader> _playerFactory;
        private IFactory<IGameCamera> _gameCameraFactory;
        private IFactory<IObstacleManagerViewport> _obstacleManagerFactory;
        private IFactory<IAnimatedGameFinisher> _gameFinisherFactory;
        private IFactory<IParticleGameBackground> _gameBackgroundFactory;
        private IFactory<IPlayerPositionScoreCalculator> _calculatorFactory;

        private CreatedObjects _context;

        public PausedGameplayFactory(IGameThemeAppenderComposite themeApplier,
            IFactory<IPlayerBlendingShaderMaintainer> shaderMaintainerFactory,
            IFactory<IPlayerWithShader> playerFactory,
            IFactory<IObstacleManagerViewport> obstacleManagerFactory, IFactory<IGameCamera> gameCameraFactory,
            IFactory<IAnimatedGameFinisher> gameFinisherFactory,
            IFactory<IParticleGameBackground> gameBackgroundFactory,
            IFactory<IPlayerPositionScoreCalculator> calculatorFactory)
        {
            _themeApplier = themeApplier;
            _shaderMaintainerFactory = shaderMaintainerFactory;
            _playerFactory = playerFactory;
            _obstacleManagerFactory = obstacleManagerFactory;
            _gameCameraFactory = gameCameraFactory;
            _gameFinisherFactory = gameFinisherFactory;
            _gameBackgroundFactory = gameBackgroundFactory;
            _calculatorFactory = calculatorFactory;
            
            _context = new CreatedObjects();
        }

        public IGameplay CreateNew()
        {
            CreateDependencies();
            InjectDependencies();
            AddThemeAppenders();
            return CreateNewPausedGame();
        }

        private void CreateDependencies()
        {
            _context = new CreatedObjects()
            {
                ShaderMaintainer = _shaderMaintainerFactory.CreateNew(),
                Background = _gameBackgroundFactory.CreateNew(),
                GameCamera = _gameCameraFactory.CreateNew(),
                GameFinisher = _gameFinisherFactory.CreateNew(),
                ObstacleManager = _obstacleManagerFactory.CreateNew(),
                Player = _playerFactory.CreateNew(),
                ScoreCalculator = _calculatorFactory.CreateNew()
            };
        }

        private void InjectDependencies()
        {
            // player
            _context.Player.ShaderMaintainer = _context.ShaderMaintainer;
            _context.Player.ObstaclesSource = _context.ObstacleManager;

            // obstacle manager
            var despawner = _context.ObstacleManager.ObstacleDespawner;
            despawner.PlayerTransform = _context.Player.Transform;
            despawner.PlayerBlendingRadius = _context.ShaderMaintainer.MaintainedShader.TotalBlendingRadius;
            
            // game camera
            _context.GameCamera.Target = _context.Player.Transform;
            
            // game finisher
            var animatedFinisher = _context.GameFinisher;
            animatedFinisher.Player = _context.Player;
            animatedFinisher.CameraController = _context.GameCamera.CameraController;
            animatedFinisher.PlayerShader = _context.ShaderMaintainer.MaintainedShader;
            
            // score calculator
            _context.ScoreCalculator.PlayerTransform = _context.Player.Transform;
        }

        private void AddThemeAppenders()
        {
            _themeApplier.Add(_context.GameCamera);
            _themeApplier.Add(_context.Player);
            _themeApplier.Add(_context.Background);
            _themeApplier.Add(_context.ObstacleManager);
        }

        private IGameplay CreateNewPausedGame()
        {
            var game = new Gameplay(
                _context.Player,
                _context.GameCamera,
                _context.ObstacleManager,
                _context.GameFinisher,
                _context.Background,
                _context.ScoreCalculator
            );

            game.Pause();

            return game;
        }

        private struct CreatedObjects
        {
            public IPlayerBlendingShaderMaintainer ShaderMaintainer;
            public IPlayerWithShader Player;
            public IObstacleManagerViewport ObstacleManager;
            public IGameCamera GameCamera;
            public IAnimatedGameFinisher GameFinisher;
            public IParticleGameBackground Background;
            public IPlayerPositionScoreCalculator ScoreCalculator;
        }
    }
}