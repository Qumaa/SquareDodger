using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Game
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;

        private bool _directionRight;
        private IPlayerInputService _inputService;
        private Rigidbody2D _rigidbody;
        

        private void Start()
        {
            InitializeInputService();
            _inputService.OnTurnInput += Turn;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        // TODO: move to bootstrap
        private void InitializeInputService() =>
            _inputService = GetComponent<IPlayerInputService>();

       
       
        private Vector2 GetDirectionVector() =>
            _directionRight ?
                new Vector2(_movementSpeed, 0) :
                new Vector2(0,_movementSpeed );

        private void Turn()
        {
            var vector = GetDirectionVector();
            _directionRight = !_directionRight;
            _rigidbody.velocity = vector;

        }
        
    }
}