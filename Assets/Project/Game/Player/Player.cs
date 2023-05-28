using System;
using UnityEngine;

namespace Project.Game
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private float _movementSpeed;

        private bool _directionRight;
        private Rigidbody2D _rigidbody;
        private IPlayerInputService _inputService;
        private Material _material;

        public event Action OnTurned;

        public Transform Transform => transform;

        public IPlayerInputService InputService
        {
            get => _inputService;
            set => UpdateInputService(value);
        }

        public Material Material => _material;

        private void Awake()
        {
            _material = GetComponent<SpriteRenderer>().material;
            _rigidbody = GetComponent<Rigidbody2D>();
            UpdateVelocity();
        }

        private Vector2 GetDirectionVector() =>
            _directionRight ?
                new Vector2(_movementSpeed, 0) :
                new Vector2(0, _movementSpeed);

        private void Turn()
        {
            UpdateVelocity();
            _directionRight = !_directionRight;

            OnTurned?.Invoke();
        }

        private void UpdateVelocity() =>
            _rigidbody.velocity = GetDirectionVector();

        private void UpdateInputService(IPlayerInputService newService)
        {
            if (_inputService == newService)
                return;
            
            if (_inputService != null)
                _inputService.OnTurnInput -= Turn;
            
            _inputService = newService;
            _inputService.OnTurnInput += Turn;
        }
    }
}