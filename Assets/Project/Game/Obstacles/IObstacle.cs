using System;
using Project.Architecture;
using UnityEngine;

namespace Project.Game
{
    public interface IObstacle : IPoolerTarget, IPausable
    {
        event Action<IObstacle> OnDespawned;
        float Size { get; }
        Vector2 Velocity { get; set; }
        Vector2 Position { get; set; }
        void Init();
        void Despawn();
    }
}