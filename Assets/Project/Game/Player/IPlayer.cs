using System;
using Project.Architecture;
using UnityEngine;

namespace Project.Game
{
    public interface IPlayer : IPausableAndResettable, IGameInputServiceConsumer
    {
        public event Action OnTurned;
        public event Action OnDied;
        public Transform Transform { get; }
        float MovementSpeed { get; set; }
        float TrailLength { get; set; }
    }
}