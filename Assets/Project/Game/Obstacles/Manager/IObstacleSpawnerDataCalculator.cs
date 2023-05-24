using UnityEngine;

namespace Project
{
    public interface IObstacleSpawnerDataCalculator
    {
        Vector3 CalculatePosition();
        Vector2 CalculateVelocity();
    }
}