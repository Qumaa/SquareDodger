using UnityEngine;

namespace Project.Game
{
    public interface IGameCamera
    {
        Vector2 Position { get; set; }
        void Update(float timeStep);
    }
}