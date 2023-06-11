using System;

namespace Project.Game
{
    public interface IObstacleSpawner
    {
        event Action<IObstacle> OnSpawned;
        IObstacleColorSource ColorSource { set; get; }
        bool ShouldSpawn { get; }
        float SpawningInterval { get; }
        void SpawnAndInit();
    }
}