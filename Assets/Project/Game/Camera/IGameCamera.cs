using UnityEngine;

namespace Project.Game
{
    public interface IGameCamera : IFixedUpdatable, IPausableAndResettable, IGameThemeAppender
    {
        ICameraController CameraController { get; }
        Transform Target { get; set; }
        Vector2 Position { get; }
    }
}