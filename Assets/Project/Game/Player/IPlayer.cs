using System;
using UnityEngine;

namespace Project.Game
{
    public interface IPlayer : IPausableAndResettable, IGameInputServiceConsumer, IGameThemeAppender
    {
        public event Action OnTurned;
        public event Action OnDied;
        public Transform Transform { get; }
        float MovementSpeed { get; set; }
        float TrailLength { get; set; }
    }
}