using System;
using Project.Game;
using UnityEngine;

namespace Project
{
    public class Obstacle : MonoBehaviour, IObstacle, IPoolerTarget
    {
        private bool _active;
        private Rigidbody2D _rigidbody;
        
        public event Action<IObstacle> OnDespawned;

        private void Awake()
        {
            _active = true;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void SetVelocity(Vector2 velocity) =>
            _rigidbody.velocity = velocity;

        void IPoolerTarget.PoppedFromPool() =>
            UpdateActiveStatus(true);

        void IPoolerTarget.PushedToPool() =>
            UpdateActiveStatus(false);

        private void UpdateActiveStatus(bool status)
        {
            if (_active == status)
                return;
            
            _active = status;
            
            gameObject.SetActive(_active);
        }
    }
}