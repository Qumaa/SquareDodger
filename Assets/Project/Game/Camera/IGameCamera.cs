using Project.Architecture;
using UnityEngine;

namespace Project.Game
{
    public interface IGameCamera : IFixedUpdatable, IPausableAndResettable
    {
        Transform Target { get; set; }
        Vector2 Position { get; }
    }
}