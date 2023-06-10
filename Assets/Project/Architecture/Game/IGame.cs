﻿using Project.Game;
using Project.UI;

namespace Project.Architecture
{
    public interface IGame : IUpdatableAnFixedUpdatable
    {
        IGameplay Gameplay { get; set; }
        IGameCanvasUIRenderer GameCanvasUI { get; set; }
        ICameraController CameraController { get; set; }
        IGameInputService InputService { get; set; }
        void Initialize();
    }
}