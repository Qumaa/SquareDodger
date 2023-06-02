using System;
using UnityEngine;

namespace Project.Game
{
    public class Obstacle : MonoBehaviour, IObstacle
    {
        private bool _active;
        private Rigidbody2D _rigidbody;

        private Vector2 _velocityBeforePausing;
        
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

        public void Init()
        {
            UpdateActiveStatus(true);
        }

        public void Despawn()
        {
            UpdateActiveStatus(false);
            OnDespawned?.Invoke(this);
        }


        private void UpdateActiveStatus(bool status)
        {
            if (_active == status)
                return;
            
            _active = status;
            
            gameObject.SetActive(_active);
        }

        public void Pause()
        {
            _velocityBeforePausing = Velocity;
            Velocity = Vector2.zero;
        }

        public void Resume()
        {
            Velocity = _velocityBeforePausing;
        }

        public void PoppedFromPool()
        {
        }

        public void PushedToPool()
        {
        }
    }
}