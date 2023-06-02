using System;
using Project.Architecture;
using UnityEngine;

namespace Project.Game
{
    public interface IPlayer : IPausableAndResettable
    {
        public event Action OnTurned;
        public event Action OnDied;
        public Transform Transform { get; }
        public IPlayerInputService InputService { get; set; }
        float MovementSpeed { get; set; }
    }
}