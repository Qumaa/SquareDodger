using UnityEngine;

namespace Project
{
    public interface IObstacleSpawner
    {
        int SpawnedObstacles { get; }
        IObstacle Spawn(Vector3 position);
    }
}