using System;
using Project.Game;
using UnityEngine;

namespace Project
{
    public class Obstacle : MonoBehaviour, IObstacle, IPoolerTarget
    {
        public event Action<IObstacle> OnDespawned;
        public void ResetToDefault()
        {
            throw new NotImplementedException();
        }

        public void Pool()
        {
            OnDespawned?.Invoke(this);
        }
    }
}