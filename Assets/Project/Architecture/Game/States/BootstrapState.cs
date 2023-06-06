﻿using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class BootstrapState : GameState
    {
        private IGame _gameToInit;
        private IDisposer _disposer;
        private GameConfig _gameConfig;
        private GameObject _uiPrefab;
        private readonly Camera _controlledCamera;

        public BootstrapState(IGameStateMachine stateMachine, IGame gameToInit, IDisposer disposer,
            GameConfig gameConfig, GameObject uiPrefab, Camera controlledCamera)
            : base(stateMachine)
        {
            _gameToInit = gameToInit;
            _disposer = disposer;
            _gameConfig = gameConfig;
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
            _gameToInit.CameraController = CreateCameraController();
            // sound, input etc.
        }

        private void LoadGame() =>
            CreateGameLoader().Load(_gameToInit);

        private void MoveToMenu() =>
            _stateMachine.SetState<InitializeMenuState>();

        private ICameraController CreateCameraController()
        {
            var controller = new CameraController(_controlledCamera, _gameConfig.CameraConfig.ViewportDepth)
            {
                WidthInUnits = _gameConfig.CameraConfig.ViewportWidth
            };

            controller.ControlledCamera.backgroundColor = _gameConfig.GameColors.BackgroundColor;
            return controller;
        }

        private IGameLoader CreateGameLoader() =>
            new PrefabGameLoader(CreateGameplayFactory(_gameToInit.CameraController), new MainMenuFactory(_uiPrefab));

        private IFactory<IGameplay> CreateGameplayFactory(ICameraController cameraController)
        {
            var shaderFactory = new PlayerShaderMaintainerFactory(_disposer);
            
            var playerFactory = new PlayerWithShaderFactory
                (_gameConfig.PlayerConfig, _gameConfig.GameColors.PlayerColor, _gameConfig.GameColors.ObstaclesColor);
            
            var gameCameraFactory = new GameCameraFactory(_gameConfig.CameraConfig, cameraController);
            
            var obstacleManagerFactory = CreateObstacleManagerFactory
                (_gameConfig.ManagerConfig, cameraController.ControlledCamera, _gameConfig.CameraConfig.ViewportDepth);
            
            var gameFinisherFactory = new GameFinisherFactory();
            
            var gameBackgroundFactory = CreateGameBackgroundFactory();

            var gameplayFactory = new PausedGameplayFactory(shaderFactory, playerFactory, obstacleManagerFactory, 
                gameCameraFactory, gameFinisherFactory, gameBackgroundFactory);

            return gameplayFactory;
        }

        private ObstacleManagerViewportFactory CreateObstacleManagerFactory(ObstacleManagerConfig managerConfig,
            Camera controlledCamera, float cameraViewportDepth)
        {
            var pooler = new ObstaclePooler();
            var factory = new ObstacleFactory(managerConfig.ObstaclePrefab.gameObject);
            var despawner = new ObstacleDespawnerViewportShader(controlledCamera,
                new Vector2(0.707f, 0.707f) * managerConfig.ObstaclePrefab.Size, pooler);

            var spawnerFactory = new ObstacleManagerSpawnerFactory
                (managerConfig, controlledCamera, cameraViewportDepth, pooler, factory);
            var managerFactory = new ObstacleManagerViewportFactory
                (spawnerFactory, despawner, _gameConfig.GameColors.ObstaclesColor);

            return managerFactory;
        }

        private IFactory<IParticleGameBackground> CreateGameBackgroundFactory()
        {
            var particleFactory =
                new GameBackgroundParticleSystemFactory(_gameConfig.VisualsConfig.BackgroundParticlesPrefab, _gameConfig.GameColors.BackgroundParticlesColor);
            var backgroundFactory = 
                new ParticleGameBackgroundFactory(particleFactory, _gameConfig.VisualsConfig.BackgroundParticlesSquareSize);
            return backgroundFactory;
        }
    }
}