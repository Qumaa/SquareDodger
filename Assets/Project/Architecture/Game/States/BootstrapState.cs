using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class BootstrapState : GameState
    {
        private IGame _gameToInit;
        private IDisposer _disposer;
        private GameConfig _gameConfig;
        private GameObject _uiPrefab;
        private Camera _controlledCamera;

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
            InitializeServices();
            MoveToMenu();
        }

        private void InitializeServices()
        {
            _gameToInit.GameLoader = CreateGameLoader(_controlledCamera);
            // sound, input etc.
        }

        private void MoveToMenu()
        {
            _stateMachine.SetState<InitializeMenuState>();
        }

        public override void Exit()
        {
            
        }
        
        IGameLoader CreateGameLoader(Camera controlledCamera)
        {
            return new PrefabGameLoader(CreateGameplayFactory(controlledCamera), new MainMenuFactory(_uiPrefab));
        }
        
        private IFactory<IGameplay> CreateGameplayFactory(Camera controlledCamera)
        {
            var gameCamera = new GameCameraFactory(_gameConfig.CameraConfig, controlledCamera).CreateNew();
            
            var shaderFactory = new PlayerShaderMaintainerFactory(_disposer);
            var playerFactory = new PlayerFactory(_gameConfig.PlayerConfig, shaderFactory);
            var obstacleManagerFactory = CreateObstacleManagerFactory
                (_gameConfig.ManagerConfig, controlledCamera, _gameConfig.CameraConfig.ViewportDepth);

            var gameplayFactory =
                new PausedGameplayFactory(shaderFactory, playerFactory, obstacleManagerFactory, gameCamera);

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
            var managerFactory = new ObstacleManagerViewportFactory(spawnerFactory, despawner);

            return managerFactory;
        }
    }
}