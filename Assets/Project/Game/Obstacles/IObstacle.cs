using System;
using UnityEngine;

namespace Project.Game
{
    public interface IObstacle : IPoolerTarget
    {
        event Action<IObstacle> OnDespawned;
        float Size { get; }
        Vector2 Velocity { get; set; }
        Vector2 Position { get; set; }
        void Despawn();
    }
}