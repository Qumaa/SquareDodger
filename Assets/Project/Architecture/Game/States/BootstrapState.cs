using DG.Tweening;
using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class BootstrapState : GameState
    {
        private IDisposer _disposer;
        private GameRuntimeData _gameData;
        private Camera _controlledCamera;

        public BootstrapState(IGameStateMachine stateMachine, IGame game, IDisposer disposer,
            GameRuntimeData gameData, Camera controlledCamera)
            : base(stateMachine, game)
        {
            _disposer = disposer;
            _gameData = gameData;
            _controlledCamera = controlledCamera;
        }

        public override void Enter()
        {
            InitializeGameServices();
            LoadGame();
            MoveNext();
        }

        public override void Exit()
        {
            
        }

        private void InitializeGameServices()
        {
            DOTween.Init();
            _game.CameraController = CreateCameraController();
            // sound, input etc.
        }

        private void LoadGame() =>
            _game.Gameplay = CreateGameplayFactory().CreateNew();

        private void MoveNext() =>
            _stateMachine.SetState<InitializeUIState>();

        private ICameraController CreateCameraController()
        {
            var controller = new CameraController(_controlledCamera, _gameData.GameCameraData.ViewportDepth)
            {
                WidthInUnits = _gameData.GameCameraData.ViewportWidth
            };

            controller.ControlledCamera.backgroundColor = _gameData.GameColorsData.BackgroundColor;
            return controller;
        }

        private IFactory<IGameplay> CreateGameplayFactory()
        {
            var shaderData = _gameData.PlayerData.ShaderData;
            var shaderFactory = new BlendingShaderFactory(shaderData.BlendingRadius, shaderData.BlendingLength,
                _gameData.GameColorsData.PlayerColor, _gameData.GameColorsData.ObstaclesColor);
            var maintainerFactory = new PlayerBlendingShaderMaintainerFactory(_disposer, shaderFactory,
                shaderData.BlendingRadius, shaderData.BlendingLength);
            
            var playerFactory = new PlayerWithShaderFactory(_gameData.PlayerData, _game.InputService);
            
            var gameCameraFactory = new GameCameraFactory(_gameData.GameCameraData, _game.CameraController);
            
            var obstacleManagerFactory = CreateObstacleManagerFactory
                (_gameData.ObstacleManagerData, _game.CameraController.ControlledCamera, _gameData.GameCameraData.ViewportDepth);
            
            var gameFinisherFactory = new DoTweenGameFinisherFactory();
            
            var gameBackgroundFactory = CreateGameBackgroundFactory();

            var calculatorFactory = new ScoreCalculatorFactory();

            var gameplayFactory = new PausedGameplayFactory(maintainerFactory, playerFactory, obstacleManagerFactory, 
                gameCameraFactory, gameFinisherFactory, gameBackgroundFactory, calculatorFactory);

            return gameplayFactory;
        }

        private ObstacleManagerViewportFactory CreateObstacleManagerFactory(ObstacleManagerRuntimeData managerConfig,
            Camera controlledCamera, float cameraViewportDepth)
        {
            var pooler = new ObstaclePooler();
            var factory = new ObstacleFactory(managerConfig.ObstaclePrefab.gameObject);
            var despawner = new ObstacleDespawnerViewportShader(controlledCamera,
                new Vector2(0.707f, 0.707f) * managerConfig.ObstaclePrefab.Size, pooler);

            var spawnerFactory = new ObstacleManagerSpawnerFactory
                (managerConfig, controlledCamera, cameraViewportDepth, pooler, factory);
            var managerFactory = new ObstacleManagerViewportFactory
                (spawnerFactory, despawner, _gameData.GameColorsData.ObstaclesColor);

            return managerFactory;
        }

        private IFactory<IParticleGameBackground> CreateGameBackgroundFactory()
        {
            var particleFactory = new GameBackgroundParticleSystemFactory(
                _gameData.GameBackgroundData.BackgroundParticlesPrefab, 
                _gameData.GameColorsData.BackgroundParticlesColor);

            var backgroundSize = 
                new ViewportBackgroundSizeCalculator(_controlledCamera, _gameData.GameBackgroundData.ParticlesAreaExtraSize)
                .Calculate();
            var backgroundFactory = new ParticleGameBackgroundFactory(particleFactory, 
                backgroundSize,
                _gameData.GameBackgroundData.DensityPerUnit);
            
            return backgroundFactory;
        }
    }
}