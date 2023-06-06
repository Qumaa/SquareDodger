using Project.Architecture;

namespace Project.Game
{
    public interface IObstacleManager : IUpdatable, IPausableAndResettable, IObstacleColorSource
    {
        IObstacle[] ActiveObstacles { get; }
    }
}