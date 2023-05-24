namespace Project
{
    public interface IObstacleSpawner
    {
        bool ShouldSpawn { get; }
        float SpawningInterval { get; }
        IObstacle Spawn();
    }
}