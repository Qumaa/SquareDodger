using Project.Game;

namespace Project.Architecture
{
    public struct PausedGameplayFactory : IFactory<IGameplay>
    {
        private readonly IGameThemeApplierComposite _themeApplier;
        private readonly IGame _game;
        private readonly GameRuntimeData _gameData;
        private readonly IDisposer _disposer;

        private IFactory<IBlendingShaderMaintainer> _shaderMaintainerFactory;
        private IFactory<IBlendingShaderPlayer> _playerFactory;
        private IFactory<IGameCamera> _gameCameraFactory;
        private IFactory<IObstacleManagerViewport> _obstacleManagerFactory;
        private IFactory<IAnimatedGameFinisher> _gameFinisherFactory;
        private IFactory<IParticleGameBackground> _gameBackgroundFactory;
        private IFactory<IPlayerPositionScoreCalculator> _calculatorFactory;

        private CreatedObjects _context;

        public PausedGameplayFactory(IGameThemeApplierComposite themeApplier, IGame game, GameRuntimeData gameData,
            IDisposer disposer)
        {
            _themeApplier = themeApplier;
            _game = game;
            _gameData = gameData;
            _disposer = disposer;

            _shaderMaintainerFactory = new PlayerBlendingShaderMaintainerFactory(_disposer,
                _gameData.PlayerData.ShaderData.BlendingRadius, _gameData.PlayerData.ShaderData.BlendingLength);
            _playerFactory = new PlayerWithShaderFactory(_gameData.PlayerData, _game.InputService);
            _obstacleManagerFactory = new ObstacleManagerViewportFactory(_gameData.ObstacleManagerData,
                _game.CameraController.ControlledCamera, _gameData.GameCameraData.ViewportDepth);
            _gameCameraFactory = new GameCameraFactory(_gameData.GameCameraData, _game.CameraController);
            _gameFinisherFactory = new DoTweenGameFinisherFactory();
            _gameBackgroundFactory = new ParticleGameBackgroundFactory(_gameData, _game.CameraController.ControlledCamera,
                _gameData.GameBackgroundData.DensityPerUnit);
            _calculatorFactory = new ScoreCalculatorFactory();

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
                PlayerShaderMaintainer = _shaderMaintainerFactory.CreateNew(),
                TrailShaderMaintainer = _shaderMaintainerFactory.CreateNew(),
                Background = _gameBackgroundFactory.CreateNew(),
                GameCamera = _gameCameraFactory.CreateNew(),
                GameFinisher = _gameFinisherFactory.CreateNew(),
                ObstacleManager = _obstacleManagerFactory.CreateNew(),
                Player = _playerFactory.CreateNew(),
                ScoreCalculator = _calculatorFactory.CreateNew(),
                GameSounds = new GameSounds() //TODO
            };
        }

        private void InjectDependencies()
        {
            // player
            _context.Player.PlayerShaderMaintainer = _context.PlayerShaderMaintainer;
            _context.Player.TrailShaderMaintainer = _context.TrailShaderMaintainer;
            _context.Player.ObstaclesSource = _context.ObstacleManager;

            // obstacle manager
            var despawner = _context.ObstacleManager.ObstacleDespawner;
            despawner.PlayerTransform = _context.Player.Transform;
            despawner.PlayerBlendingRadius = _context.PlayerShaderMaintainer.MaintainedShader.TotalBlendingRadius;

            // game camera
            _context.GameCamera.Target = _context.Player.Transform;

            // game finisher
            var animatedFinisher = _context.GameFinisher;
            animatedFinisher.Player = _context.Player;
            animatedFinisher.CameraController = _context.GameCamera.CameraController;
            animatedFinisher.PlayerShader = _context.PlayerShaderMaintainer.MaintainedShader;

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
            var game = new Gameplay(_context.Player, _context.GameCamera, _context.ObstacleManager,
                _context.GameFinisher, _context.Background, _context.ScoreCalculator, _context.GameSounds);

            game.Pause();

            return game;
        }

        private struct CreatedObjects
        {
            public IBlendingShaderMaintainer PlayerShaderMaintainer;
            public IBlendingShaderMaintainer TrailShaderMaintainer;
            public IBlendingShaderPlayer Player;
            public IObstacleManagerViewport ObstacleManager;
            public IGameCamera GameCamera;
            public IAnimatedGameFinisher GameFinisher;
            public IParticleGameBackground Background;
            public IPlayerPositionScoreCalculator ScoreCalculator;
            public IGameSounds GameSounds;
        }
    }
}