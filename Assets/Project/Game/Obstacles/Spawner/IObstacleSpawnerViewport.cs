namespace Project.Game
{
    public interface IObstacleSpawnerViewport : IObstacleSpawner
    {
        public ObstacleSpawnerDataViewport Data { get; }
    }
}