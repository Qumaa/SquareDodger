using UnityEngine;

namespace Project.Game
{
    public class PlayerShaderRuntimeData : ILoadableFrom<PlayerConfig>, ILoadableFrom<GameColorsConfig>
    {
        public float BlendingRadius { get; private set; }
        public float BlendingLength { get; private set; }
        public Color32 PlayerColor { get; private set; }
        public Color32 BlendingColor { get; private set; }
        
        public void Load(PlayerConfig data)
        {
            BlendingRadius = data.BlendingRadius;
            BlendingLength = data.BlendingLength;
        }

        public void Load(GameColorsConfig data)
        {
            PlayerColor = data.PlayerColor;
            BlendingColor = data.ObstaclesColor;
        }
    }
}