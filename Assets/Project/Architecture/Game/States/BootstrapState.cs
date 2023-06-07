using DG.Tweening;
using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class BootstrapState : GameState
    {
        private IGame _gameToInit;
        private IDisposer _disposer;
        private GameRuntimeData _gameData;
        private GameObject _uiPrefab;
        private Camera _controlledCamera;

        public BootstrapState(IGameStateMachine stateMachine, IGame gameToInit, IDisposer disposer,
            GameRuntimeData gameData, GameObject uiPrefab, Camera controlledCamera)
            : base(stateMachine)
        {
            _gameToInit = gameToInit;
            _disposer = disposer;
            _gameData = gameData;
            _uiPrefab = uiPrefab;
            _controlledCamera = controlledCamera;
        }

        public override void Enter()
        {
            InitializeGameServices();
            LoadGame();
            MoveToMenu();
        }

        public override void Exit()
        {
            
        }

        private void InitializeGameServices()
        {
            DOTween.Init();
            _gameToInit.CameraController = CreateCameraController();
            // sound, input etc.
        }

        private void LoadGame() =>
            CreateGameLoader().Load(_gameToInit);

        private void MoveToMenu() =>
            _stateMachine.SetState<InitializeMenuState>();

        private ICameraController CreateCameraController()
        {
            var controller = new CameraController(_controlledCamera, _gameData.GameCameraData.ViewportDepth)
            {
                WidthInUnits = _gameData.GameCameraData.ViewportWidth
            };

            controller.ControlledCamera.backgroundColor = _gameData.GameColorsData.BackgroundColor;
            return controller;
        }

        private IGameLoader CreateGameLoader() =>
            new PrefabGameLoader(CreateGameplayFactory(_gameToInit.CameraController), new MainMenuFactory(_uiPrefab));

        private IFactory<IGameplay> CreateGameplayFactory(ICameraController cameraController)
        {
            var shaderFactory = new PlayerShaderMaintainerFactory(_disposer);
            
            var playerFactory = new PlayerWithShaderFactory
                (_gameData.PlayerData, _gameData.GameColorsData.PlayerColor, _gameData.GameColorsData.ObstaclesColor);
            
            var gameCameraFactory = new GameCameraFactory(_gameData.GameCameraData, cameraController);
            
            var obstacleManagerFactory = CreateObstacleManagerFactory
                (_gameData.ObstacleManagerData, cameraController.ControlledCamera, _gameData.GameCameraData.ViewportDepth);
            
            var gameFinisherFactory = new DoTweenGameFinisherFactory();
            
            var gameBackgroundFactory = CreateGameBackgroundFactory();

            var gameplayFactory = new PausedGameplayFactory(shaderFactory, playerFactory, obstacleManagerFactory, 
                gameCameraFactory, gameFinisherFactory, gameBackgroundFactory);

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