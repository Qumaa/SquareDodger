using UnityEngine;

namespace Project.Game
{
    public class GameColorsRuntimeData : ILoadableFrom<GameColorsConfig>
    {
        public Color32 PlayerColor { get; private set; }
        public Color32 ObstaclesColor { get; private set; }
        public Color32 BackgroundColor { get; private set; }
        public Color32 BackgroundParticlesColor { get; private set; }
        
        public void Load(GameColorsConfig data)
        {
            PlayerColor = data.PlayerColor;
            ObstaclesColor = data.ObstaclesColor;
            BackgroundColor = data.BackgroundColor;
            BackgroundParticlesColor = data.BackgroundParticlesColor;
        }
    }
}