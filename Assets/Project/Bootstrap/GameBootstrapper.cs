using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Game;

namespace Project.Architecture
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private GameCameraConfig _cameraConfig;
        [SerializeField] private ObstacleManagerConfig _managerConfig;
        [SerializeField] private Player _playerPrefab;

        private IGameCamera _gameCamera;
        private Player _player;
        private IObstacleManager _obstacleManager;

        private void Awake()
        {
            InitializePlayer();
            InitializeCamera(_player.transform);
            InitializeObstacleManager();
        }

        private void FixedUpdate()
        {
            _gameCamera.Update(Time.deltaTime);
        }

        private void Update()
        {
            _obstacleManager.Update(Time.deltaTime);
        }

        private void InitializePlayer()
        {
            var playerObj = Instantiate(_playerPrefab.gameObject);
            
            _player = playerObj.GetComponent<Player>();
            _player.InputService = playerObj.GetComponent<IPlayerInputService>();
        }

        private void InitializeCamera(Transform playerTransform)
        {
            var controlledCamera = Camera.main;
            
            _gameCamera = new GameCamera(
                controlledCamera,
                _cameraConfig.ViewportDepth,
                new ProceduralMotionSystemVector2(_cameraConfig.MotionSpeed, _cameraConfig.MotionDamping,
                    _cameraConfig.MotionResponsiveness),
                new CameraOffsetCalculatorViewport(controlledCamera),
                playerTransform,
                _cameraConfig.BottomOffset
            );
        }

        private void InitializeObstacleManager()
        {
            var pooler = new ObstaclePooler();
            var factory = new ObstacleFactory(_managerConfig.ObstaclePrefab.gameObject);
            var despawner = new ObstacleDespawnerViewport(_gameCamera.ControlledCamera,
                new Vector2(0.707f, 0.707f) * _managerConfig.ObstaclePrefab.Size);
            
            _obstacleManager = new ObstacleManagerFactory(_managerConfig, _gameCamera, pooler, factory, despawner, this)
                .CreateNew();
        }
    }
}
