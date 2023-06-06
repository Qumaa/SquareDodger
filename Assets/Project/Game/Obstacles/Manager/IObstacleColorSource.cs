using UnityEngine;

namespace Project.Game
{
    public interface IObstacleColorSource
    {
        public Color32 ObstacleColor { get; }
    }
}