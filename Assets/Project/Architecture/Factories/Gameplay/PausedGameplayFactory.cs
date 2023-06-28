using Project.Game;

namespace Project.Architecture
{
    public struct PausedGameplayFactory : IFactory<IGameplay>
    {
        private readonly IGameThemeApplierComposite _themeApplier;
        private readonly IGameSounds _gameSounds;

        private readonly IFactory<IBlendingShaderMaintainer> _playerShaderMaintainerFactory;
        private readonly IFactory<IBlendingShaderMaintainer> _trailShaderMaintainerFactory;
        private readonly IFactory<IBlendingShaderPlayer> _playerFactory;
        private readonly IFactory<IGameCamera> _gameCameraFactory;
        private readonly IFactory<IObstacleManagerViewport> _obstacleManagerFactory;
        private readonly IFactory<IAnimatedGameFinisher> _gameFinisherFactory;
        private readonly IFactory<IParticleGameBackground> _gameBackgroundFactory;
        private readonly IFactory<IPlayerPositionScoreCalculator> _calculatorFactory;

        private CreatedObjects _context;

        public PausedGameplayFactory(IGameThemeApplierComposite themeApplier, IGame game, GameRuntimeData gameData,
            IDisposer disposer)
        {
            _themeApplier = themeApplier;
            _gameSounds = game.GameSounds;

            _playerShaderMaintainerFactory = new BlendingShaderMaintainerFactory(disposer,
                gameData.PlayerData.ShaderData.BlendingRadius, gameData.PlayerData.ShaderData.BlendingLength, gameData.PlayerData.PlayerMaterial);
            _trailShaderMaintainerFactory = new BlendingShaderMaintainerFactory(disposer,
                gameData.PlayerData.ShaderData.BlendingRadius, gameData.PlayerData.ShaderData.BlendingLength, gameData.PlayerData.TrailMaterial);
            _playerFactory = new PlayerWithShaderFactory(gameData.PlayerData, game.InputService);
            _obstacleManagerFactory = new ObstacleManagerViewportFactory(gameData.ObstacleManagerData,
                game.CameraController.ControlledCamera, gameData.GameCameraData.ViewportDepth);
            _gameCameraFactory = new GameCameraFactory(gameData.GameCameraData, game.CameraController);
            _gameFinisherFactory = new DoTweenGameFinisherFactory();
            _gameBackgroundFactory = new ParticleGameBackgroundFactory(gameData, game.CameraController.ControlledCamera,
                gameData.GameBackgroundData.DensityPerUnit);
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
                PlayerShaderMaintainer = _playerShaderMaintainerFactory.CreateNew(),
                TrailShaderMaintainer = _trailShaderMaintainerFactory.CreateNew(),
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
                _context.GameFinisher, _context.Background, _context.ScoreCalculator, _gameSounds);

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
        }
    }
}