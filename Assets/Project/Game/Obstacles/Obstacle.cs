using System;
using UnityEngine;

namespace Project.Game
{
    public class Obstacle : MonoBehaviour, IObstacle
    {
        private bool _active;
        private Rigidbody2D _rigidbody;
        
        public event Action<IObstacle> OnDespawned;

        public float Size => 1;

        public Vector2 Velocity
        {
            get => _rigidbody.velocity;
            set => _rigidbody.velocity = value;
        }

        public Vector2 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        private void Awake()
        {
            _active = true;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Despawn() =>
            OnDespawned?.Invoke(this);

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