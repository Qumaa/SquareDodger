﻿using System;
using UnityEngine;

namespace Project.Game
{
    public interface IObstacle : IPoolerTarget, IPausable
    {
        event Action<IObstacle> OnDespawned;
        float Size { get; }
        Vector2 Velocity { get; set; }
        Vector2 Position { get; set; }
        Color32 Color { get; set; }
        void Init();
        void Despawn();
    }
}