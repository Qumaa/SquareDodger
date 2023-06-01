using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct PausedGameFactory : IFactory<IGame>
    {
        private IPlayerShaderMaintainer _shaderMaintainer;
        private IPlayer _player;
        private IGameCamera _gameCamera;
        private IObstacleManager _obstacleManager;

        public PausedGameFactory(Camera controlledCamera, GameCameraConfig cameraConfig, ObstacleManagerConfig managerConfig,
            PlayerConfig playerConfig, IDisposer disposer)
        {
            _shaderMaintainer = InitializePlayerShaderMaintainer(disposer);
            _player = InitializePlayer(playerConfig, _shaderMaintainer);
            _gameCamera = InitializeGameCamera(controlledCamera, cameraConfig, _player.Transform);
            _obstacleManager = InitializeObstacleManager(managerConfig, _gameCamera, _player);
        }

        public IGame CreateNew()
        {
            var game = new Game(_player, _shaderMaintainer, _gameCamera, _obstacleManager);
            _player.OnDied += game.Finish;
            game.Pause();

            return game;
        }
        
        private static IPlayer InitializePlayer(PlayerConfig playerConfig, IPlayerShaderMaintainer shaderMaintainer)
        {
            var playerObj = Object.Instantiate(playerConfig.PlayerPrefab);

            var inputService = playerObj.GetComponent<IPlayerInputService>();
            var collisionDetector = playerObj.GetComponent<IPlayerCollisionDetector>();
            var player = new Player(playerObj, shaderMaintainer, collisionDetector)
            {
                InputService = inputService,
                MovementSpeed = playerConfig.MovementSpeed
            };

            return player;
        }

        private static IGameCamera InitializeGameCamera(Camera controlledCamera, GameCameraConfig cameraConfig,
            Transform playerTransform)
        {
            var gameCamera = new GameCamera(
                controlledCamera,
                cameraConfig.ViewportDepth,
                new ProceduralMotionSystemVector2(cameraConfig.MotionSpeed, cameraConfig.MotionDamping,
                    cameraConfig.MotionResponsiveness),
                new CameraOffsetCalculatorViewport(controlledCamera),
                playerTransform,
                cameraConfig.BottomOffset
            );

            return gameCamera;
        }

        private static IObstacleManager InitializeObstacleManager(ObstacleManagerConfig managerConfig, 
            IGameCamera gameCamera, IPlayer player)
        {
            var pooler = new ObstaclePooler();
            var factory = new ObstacleFactory(managerConfig.ObstaclePrefab.gameObject);
            var despawner = new ObstacleDespawnerViewportShader(gameCamera.ControlledCamera,
                new Vector2(0.707f, 0.707f) * managerConfig.ObstaclePrefab.Size, player.Transform,
                player.ShaderMaintainer.BlendingRadius);
            
            var obstacleManager = new ObstacleManagerFactory(managerConfig, gameCamera, pooler, factory, despawner)
                .CreateNew();

            return obstacleManager;
        }

        private static IPlayerShaderMaintainer InitializePlayerShaderMaintainer(IDisposer disposer)
        {
            var shaderMaintainer = new PlayerShaderMaintainer();
            disposer.Register(shaderMaintainer);

            return shaderMaintainer;
        }
    }
}