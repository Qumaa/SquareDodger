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

        private void Start()
        {
            InitializeInputService();
            _inputService.OnTurnInput += Turn;
        }

        // TODO: move to bootstrap
        private void InitializeInputService() =>
            _inputService = GetComponent<IPlayerInputService>();

        private void Update()
        {
            MakePlayerStep();
        }
        
        public void MakePlayerStep()
        {
            transform.position += GetDirectionVector() * Time.deltaTime;
        }

        private Vector3 GetDirectionVector() =>
            _directionRight ?
                new Vector3(_movementSpeed, 0, 0) :
                new Vector3(0, _movementSpeed, 0);

        private void Turn() =>
            _directionRight = !_directionRight;
    }
}