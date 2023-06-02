using Project.Architecture;

namespace Project.Game
{
    public interface IObstacleManager : IUpdatable, IPausableAndResettable
    {
        IObstacle[] ActiveObstacles { get; }
    }
}