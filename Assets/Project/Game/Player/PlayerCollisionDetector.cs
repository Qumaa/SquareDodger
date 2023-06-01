using System;
using UnityEngine;

namespace Project.Game
{
    public class PlayerCollisionDetector : MonoBehaviour, IPlayerCollisionDetector
    {
        public event Action OnCollided;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IObstacle>(out _))
            {
                OnCollided?.Invoke();
            }
        }
    }
}