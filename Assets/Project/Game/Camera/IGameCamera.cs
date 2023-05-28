using UnityEngine;

namespace Project.Game
{
    public interface IGameCamera : IUpdatable
    {
        Vector2 Position { get; set; }
        Camera ControlledCamera { get; }
        float ViewportDepth { get; }
    }
}