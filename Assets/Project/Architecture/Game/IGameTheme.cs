using UnityEngine;

namespace Project.Game
{
    public interface IGameTheme
    {
        Color32 PlayerColor { get; }
        Color32 ObstacleColor { get; }
        Color32 BackgroundColor { get; }
        Color32 ParticlesColor { get; }
    }
}