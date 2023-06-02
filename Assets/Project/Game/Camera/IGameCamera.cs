using Project.Architecture;
using UnityEngine;

namespace Project.Game
{
    public interface IGameCamera : IFixedUpdatable, IPausableAndResettable
    {
        Vector2 Position { get; set; }
        Camera ControlledCamera { get; }
        Transform Target { get; set; }
        float ViewportDepth { get; }
    }
}