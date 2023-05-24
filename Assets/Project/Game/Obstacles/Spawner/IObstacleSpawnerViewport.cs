namespace Project
{
    public interface IObstacleSpawnerViewport : IObstacleSpawner
    {
        public ObstacleSpawnerDataViewport Data { get; }
    }
}