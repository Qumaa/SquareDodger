using Project.Architecture;
using UnityEngine;

namespace Project.Game
{
    public interface IGameBackground : IUpdatable, IPausableAndResettable
    {
        Vector2 CenterPosition { get; set; }
    }
}