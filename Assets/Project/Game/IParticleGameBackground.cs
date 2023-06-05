using UnityEngine;

namespace Project.Game
{
    public interface IParticleGameBackground : IGameBackground
    {
        Vector2 Size { get; set; }
        
    }
}