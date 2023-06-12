using System;
using UnityEngine;

namespace Project.Game
{
    public class Player : PausableAndResettable, IPlayer
    {
        private bool _movingRight;
        private Rigidbody2D _rigidbody;
        private IGameInputService _inputService;
        private TrailRenderer _trailRenderer;
        private GameObject _gameObject;

        private float _movementSpeed;
        private float _movementSpeedBeforePausing;
        private float _trailLength;
        private float _trailTime;

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

        public Player(GameObject playerObject, IPlayerCollisionDetector collisionDetector, Material trailMaterial)
        {
            _gameObject = playerObject;
            _rigidbody = _gameObject.GetComponent<Rigidbody2D>();
            _trailRenderer = _gameObject.GetComponent<TrailRenderer>();
            _trailRenderer.material = trailMaterial;

            collisionDetector.OnCollided += Die;
        }

        public virtual void ApplyTheme(IGameTheme theme)
        {
            _trailRenderer.material.color = theme.PlayerColor;
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
            _trailRenderer.time = float.PositiveInfinity;
            _movementSpeedBeforePausing = _movementSpeed;
            _movementSpeed = 0;
            UpdateVelocity();
        }

        protected override void OnResumed()
        {
            _trailRenderer.time = _trailTime;
            _movementSpeed = _movementSpeedBeforePausing;
            UpdateVelocity();
        }

        protected override void OnReset()
        {
            _movingRight = false;
            Transform.position = Vector3.zero;
            _trailRenderer.Clear();
        }

        private void SetTrailLength(float length)
        {
            _trailRenderer.time = _trailTime = length / _movementSpeed;
        }
    }
}