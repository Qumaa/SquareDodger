namespace Project.Game
{
    public interface IObstacleSpawner
    {
        bool ShouldSpawn { get; }
        float SpawningInterval { get; }
        IObstacle[] ActiveObstacles { get; }
        void SpawnAndInit();
    }
}