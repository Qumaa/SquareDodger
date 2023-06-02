using UnityEngine;

namespace Project.Game
{
    public interface IObstacleDespawnerViewportShader : IObstacleDespawner
    {
        Transform Player { get; set; }
        float PlayerBlendingRadius { get; set; }
    }
}