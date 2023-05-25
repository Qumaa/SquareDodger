using UnityEngine;

namespace Project.Game
{
    public interface IObstacleSpawnerDataCalculator
    {
        Vector3 CalculatePosition();
        Vector2 CalculateVelocity();
    }
}