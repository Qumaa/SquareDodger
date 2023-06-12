using System;

namespace Project.Game
{
    public interface IObstacleSpawner
    {
        event Action<IObstacle> OnSpawned;
        event Action<IObstacle> OnCreated;
        bool ShouldSpawn { get; }
        float SpawningInterval { get; }
        void SpawnAndInit();
    }
}