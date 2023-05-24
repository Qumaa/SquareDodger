using System;
using Project.Game;
using UnityEngine;

namespace Project
{
    public class Obstacle : MonoBehaviour, IObstacle, IPoolerTarget
    {
        private bool _active;
        private Vector2 _velocity;
        
        public event Action<IObstacle> OnDespawned;

        private void Awake()
        {
            _active = true;
        }

        public void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

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