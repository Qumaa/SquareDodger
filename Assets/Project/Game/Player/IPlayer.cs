using System;
using UnityEngine;

namespace Project.Game
{
    public interface IPlayer
    {
        public event Action OnTurned;
        public Transform Transform { get; }
        public IPlayerInputService InputService { get; set; }
        Material Material { get; }
    }
}