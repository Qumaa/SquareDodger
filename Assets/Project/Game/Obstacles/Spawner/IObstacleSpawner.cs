namespace Project.Game
{
    public interface IObstacleSpawner
    {
        IObstacleColorSource ColorSource { set; get; }
        bool ShouldSpawn { get; }
        float SpawningInterval { get; }
        IObstacle[] ActiveObstacles { get; }
        void SpawnAndInit();
    }
}