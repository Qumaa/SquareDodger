using System;
using UnityEngine;

namespace Project
{
    public interface IObstacle
    {
        event Action<IObstacle> OnDespawned;
        void SetVelocity(Vector2 velocity);
    }
}