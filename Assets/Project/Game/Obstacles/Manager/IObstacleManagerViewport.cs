namespace Project.Game
{
    public interface IObstacleManagerViewport : IObstacleManager
    {
        IObstacleDespawnerViewportShader ObstacleDespawner { get; }
    }
}