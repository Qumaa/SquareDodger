using UnityEngine;

namespace Project.Game
{
    public interface IObstacleDespawnerViewportShader : IObstacleDespawner
    {
        Transform PlayerTransform { get; set; }
        float PlayerBlendingRadius { get; set; }
    }
}