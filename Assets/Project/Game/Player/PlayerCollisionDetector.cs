using System;
using UnityEngine;

namespace Project.Game
{
    public class PlayerCollisionDetector : MonoBehaviour, IPlayerCollisionDetector
    {
        public event Action OnCollided;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Obstacle _))
            {
                OnCollided?.Invoke();
            }
        }
    }
}