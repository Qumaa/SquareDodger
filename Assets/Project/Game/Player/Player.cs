using System;
using UnityEngine;

namespace Project.Game
{
    public abstract class Player : PausableAndResettable, IPlayer
    {
        private bool _movingRight;
        private Rigidbody2D _rigidbody;
        private IGameInputService _inputService;
        private GameObject _gameObject;
        protected readonly SpriteRenderer _renderer;
        protected readonly TrailRenderer _trailRenderer;

        private float _movementSpeed;
        private float _movementSpeedBeforePausing;
        private float _trailLength;

        public Transform Transform => _gameObject.transform;
        public IGameInputService InputService
        {
            get => _inputService;
            set => SetInputService(value);
        }
        public float MovementSpeed
        {
            get => _movementSpeed;
            set => SetMovementSpeed(value);
        }

        public float TrailLength
        {
            get => _trailLength;
            set => SetTrailLength(value);
        }

        public event Action OnTurned;
        public event Action OnDied;

        public Player(GameObject playerObject, IPlayerCollisionDetector collisionDetector)
        {
            _movingRight = true;
            _gameObject = playerObject;
            _rigidbody = _gameObject.GetComponent<Rigidbody2D>();
            _renderer = _gameObject.GetComponent<SpriteRenderer>();
            _trailRenderer = _gameObject.GetComponent<TrailRenderer>();

            collisionDetector.OnCollided += Die;
        }

        public abstract void ApplyTheme(IGameTheme theme);

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
                new Vector2(_movementSpeed, 0) :
                new Vector2(0, _movementSpeed);

        private void Die()
        {
            OnDied?.Invoke();
        }

        private void SetInputService(IGameInputService value)
        {
            if (_inputService == value)
                return;

            if (_inputService != null)
                _inputService.OnScreenTouchInput -= Turn;

            _inputService = value;
            _inputService.OnScreenTouchInput += Turn;
        }

        private void SetMovementSpeed(float value)
        {
            _movementSpeed = value;
            UpdateVelocity();
        }

        protected override void OnPaused()
        {
            _movementSpeedBeforePausing = _movementSpeed;
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
            _movingRight = true;
            Transform.position = Vector3.zero;
            _trailRenderer.Clear();
        }

        private void SetTrailLength(float length)
        {
            _trailRenderer.time = length / _movementSpeed;
        }
    }
}