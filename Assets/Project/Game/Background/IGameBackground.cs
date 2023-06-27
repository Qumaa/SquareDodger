using UnityEngine;

namespace Project.Game
{
    public interface IGameBackground : IUpdatable, IPausableAndResettable, IGameThemeAppender
    {
        Vector2 CenterPosition { get; set; }
        Vector2 Size { get; }
    }
}