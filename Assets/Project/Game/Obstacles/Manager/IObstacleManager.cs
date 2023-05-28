namespace Project.Game
{
    public interface IObstacleManager : IUpdatable
    {
        IObstacle[] ActiveObstacles { get; }
    }
}