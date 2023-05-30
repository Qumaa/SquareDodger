using System;
using UnityEngine;

namespace Project.Game
{
    public interface IPlayer : IUpdatable
    {
        public event Action OnTurned;
        public Transform Transform { get; }
        public IPlayerInputService InputService { get; set; }
        IPlayerShaderMaintainer ShaderMaintainer { get; }
        
        float MovementSpeed { get; set; }
    }
}