using UnityEngine;
using Project.Game;

namespace Project.Architecture
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private GameCameraConfig _cameraConfig;
        [SerializeField] private ObstacleManagerConfig _managerConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        
        private IGame _game;

        // TODO: turn into a standalone object
        private IDisposer _disposer;

        private void Start()
        {
            _disposer = new Disposer();
            InitializeGame();
        }

        private void FixedUpdate()
        {
            _game.FixedUpdate();
        }

        private void Update()
        {
            _game.Update();
        }

        private void InitializeGame()
        {
            var shaderMaintainer = InitializePlayerShaderMaintainer();
            var player = InitializePlayer(shaderMaintainer);
            var gameCamera = InitializeGameCamera(player.Transform);
            var obstacleManager = InitializeObstacleManager(gameCamera, player);

            _game = new Game(player, shaderMaintainer, gameCamera, obstacleManager);
        }

        private IPlayer InitializePlayer(IPlayerShaderMaintainer shaderMaintainer)
        {
            var playerObj = Instantiate(_playerConfig.PlayerPrefab);

            var inputService = playerObj.GetComponent<IPlayerInputService>();
            var player = new Player(playerObj, shaderMaintainer)
            {
                InputService = inputService,
                MovementSpeed = _playerConfig.MovementSpeed
            };

            return player;
        }

        private IGameCamera InitializeGameCamera(Transform playerTransform)
        {
            var controlledCamera = Camera.main;
            
            var gameCamera = new GameCamera(
                controlledCamera,
                _cameraConfig.ViewportDepth,
                new ProceduralMotionSystemVector2(_cameraConfig.MotionSpeed, _cameraConfig.MotionDamping,
                    _cameraConfig.MotionResponsiveness),
                new CameraOffsetCalculatorViewport(controlledCamera),
                playerTransform,
                _cameraConfig.BottomOffset
            );

            return gameCamera;
        }

        private IObstacleManager InitializeObstacleManager(IGameCamera gameCamera, IPlayer player)
        {
            var pooler = new ObstaclePooler();
            var factory = new ObstacleFactory(_managerConfig.ObstaclePrefab.gameObject);
            var despawner = new ObstacleDespawnerViewportShader(gameCamera.ControlledCamera,
                new Vector2(0.707f, 0.707f) * _managerConfig.ObstaclePrefab.Size, player);
            
            var obstacleManager = new ObstacleManagerFactory(_managerConfig, gameCamera, pooler, factory, despawner)
                .CreateNew();

            return obstacleManager;
        }

        private IPlayerShaderMaintainer InitializePlayerShaderMaintainer()
        {
            var shaderMaintainer = new PlayerShaderMaintainer();
            _disposer.Register(shaderMaintainer);

            return shaderMaintainer;
        }

        private void OnDestroy()
        {
            _disposer.DisposeAll();
        }
    }
}
