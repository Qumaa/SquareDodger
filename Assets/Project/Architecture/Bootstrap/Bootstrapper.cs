using System;
using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private GameObject _uiPrefab;

        private IGame _game;
        private IDisposer _disposer;

        private void Awake()
        {
            var controlledCamera = Camera.main;
            _disposer = new Disposer();
            var loader = CreateGameLoader(controlledCamera);
            _game = new Game(loader, controlledCamera);
            _game.Run();
        }

        private void Update()
        {
            _game.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _game.FixedUpdate(Time.deltaTime);
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

        private void OnDestroy()
        {
            _disposer.DisposeAll();
        }
    }
}