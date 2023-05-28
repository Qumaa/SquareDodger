using System;

namespace Project.Game
{
    public interface IPlayer
    {
        public event Action OnTurned;
        public IPlayerInputService InputService { get; set; }
    }
}