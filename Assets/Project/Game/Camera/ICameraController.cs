using UnityEngine;

namespace Project.Game
{
    public interface ICameraController
    {
        Vector2 Position { get; set; }
        float WidthInUnits { get; set; }
        Camera ControlledCamera { get; }
        float ViewportDepth { get; }
    }
}