using System;
using UnityEngine;

namespace Project.Game
{
    public class Player : PausableAndResettable, IPlayer
    {
        private bool _movingRight;
        private Rigidbody2D _rigidbody;
        private IPlayerInputService _inputService;
        protected GameObject _gameObject;
        protected TrailRenderer _trailRenderer;

        private float _movementSpeed;
        private float _movementSpeedBeforePausing;

        public Transform Transform => _gameObject.transform;
        public IPlayerInputService InputService
        {
            get => _inputService;
            set => SetInputService(value);
        }
        public float MovementSpeed
        {
            get => _movementSpeed;
            set => SetMovementSpeed(value);
        }

        public event Action OnTurned;
        public event Action OnDied;

        public Player(GameObject playerObject, IPlayerCollisionDetector collisionDetector)
        {
            _gameObject = playerObject;
            _rigidbody = _gameObject.GetComponent<Rigidbody2D>();
            _trailRenderer = _gameObject.GetComponent<TrailRenderer>();

            collisionDetector.OnCollided += Die;
        }

        private void Turn()
        {
            if (_isPaused)
                return;
            
            _movingRight = !_movingRight;
            UpdateVelocity();

            OnTurned?.Invoke();
        }

        private void UpdateVelocity() =>
            _rigidbody.velocity = CalculateVelocity();

        private Vector2 CalculateVelocity() =>
            _movingRight ?
                new Vector2(MovementSpeed, 0) :
                new Vector2(0, MovementSpeed);

        private void Die()
        {
            OnDied?.Invoke();
        }

        private void SetInputService(IPlayerInputService value)
        {
            if (_inputService == value)
                return;

            if (_inputService != null)
                _inputService.OnTurnInput -= Turn;

            _inputService = value;
            _inputService.OnTurnInput += Turn;
        }

        private void SetMovementSpeed(float value)
        {
            _movementSpeed = value;
            UpdateVelocity();
        }

        protected override void OnPaused()
        {
            _movementSpeedBeforePausing = MovementSpeed;
            _movementSpeed = 0;
            UpdateVelocity();
        }

        protected override void OnResumed()
        {
            _movementSpeed = _movementSpeedBeforePausing;
            UpdateVelocity();
        }

        protected override void OnReset()
        {
            Transform.position = Vector3.zero;
        }
    }
}