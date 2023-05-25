using System;
using UnityEngine;

namespace Project.Game
{
    public class PlayerScript : MonoBehaviour, IPlayer
    {
        [SerializeField] private float _movementSpeed;

        private bool _directionRight;
        private IPlayerInputService _inputService;
        private Rigidbody2D _rigidbody;

        public event Action OnTurned;

        private void Start()
        {
            // TODO: move to bootstrap
            _inputService = GetComponent<IPlayerInputService>();
            
            _inputService.OnTurnInput += Turn;
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
    }
}