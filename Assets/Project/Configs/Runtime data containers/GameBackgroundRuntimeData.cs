using UnityEngine;

namespace Project.Game
{
    public class GameBackgroundRuntimeData : ILoadableFrom<GameBackgroundConfig>
    {
        public GameObject BackgroundParticlesPrefab { get; private set; }
        public Vector2 ParticlesAreaExtraSize { get; private set; }
        public float DensityPerUnit { get; private set; }
        
        public void Load(GameBackgroundConfig data)
        {
            BackgroundParticlesPrefab = data.BackgroundParticlesPrefab;
            ParticlesAreaExtraSize = data.ParticlesAreaExtraSize;
            DensityPerUnit = data.DensityPerUnit;
        }
    }
}