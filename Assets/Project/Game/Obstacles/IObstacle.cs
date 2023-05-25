using System;
using UnityEngine;

namespace Project.Game
{
    public interface IObstacle : IPoolerTarget
    {
        event Action<IObstacle> OnDespawned;
        Vector2 Velocity { get; set; }
        Vector2 Position { get; set; }
        void Despawn();
    }
}