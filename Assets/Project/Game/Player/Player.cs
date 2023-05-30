using System;
using UnityEngine;

namespace Project.Game
{
    public class Player : IPlayer
    {
        // moving
        public event Action OnTurned;
        private bool _directionRight;
        private Rigidbody2D _rigidbody;
        private IPlayerInputService _inputService;
        public IPlayerInputService InputService
        {
            get => _inputService;
            set => UpdateInputService(value);
        }
        
        // shader
        private IObstacleManager _obstacleManager;
        public IPlayerShaderMaintainer ShaderMaintainer { get; }
        public Material Material { get; }

        // others
        private GameObject _gameObject;
        private float _movementSpeed;
        public Transform Transform => _gameObject.transform;
        
        public float MovementSpeed
        {
            get => _movementSpeed;
            set => UpdateMovementSpeed(value);
        }

        public void Update(float timeStep)
        {
            ShaderMaintainer.UpdateBuffer(_obstacleManager.ActiveObstacles);
        }

        public Player(GameObject playerObject, IPlayerShaderMaintainer shaderMaintainer)
        {
            _gameObject = playerObject;
            _rigidbody = _gameObject.GetComponent<Rigidbody2D>();
            
            ShaderMaintainer = shaderMaintainer;
            ShaderMaintainer.Material = Material = _gameObject.GetComponent<Renderer>().material;
        }

        private void Turn()
        {
            UpdateVelocity();
            _directionRight = !_directionRight;

            OnTurned?.Invoke();
        }

        private void UpdateVelocity() =>
            _rigidbody.velocity = CalculateVelocity();

        private Vector2 CalculateVelocity() =>
            _directionRight ?
                new Vector2(MovementSpeed, 0) :
                new Vector2(0, MovementSpeed);

        private void UpdateInputService(IPlayerInputService newService)
        {
            if (_inputService == newService)
                return;
            
            if (_inputService != null)
                _inputService.OnTurnInput -= Turn;
            
            _inputService = newService;
            _inputService.OnTurnInput += Turn;
        }

        private void UpdateMovementSpeed(float speed)
        {
            _movementSpeed = speed;
            UpdateVelocity();
        }
    }
}