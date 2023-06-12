using UnityEngine;

namespace Project.Game
{
    public class GameColorsRuntimeData : ILoadableFrom<GameColorsConfig>, IGameTheme
    {
        public Color32 PlayerColor { get; private set; }
        public Color32 ObstacleColor { get; private set; }
        public Color32 BackgroundColor { get; private set; }
        public Color32 ParticlesColor { get; private set; }
        
        public void Load(GameColorsConfig data)
        {
            PlayerColor = data.PlayerColor;
            ObstacleColor = data.ObstaclesColor;
            BackgroundColor = data.BackgroundColor;
            ParticlesColor = data.BackgroundParticlesColor;
        }
    }
}