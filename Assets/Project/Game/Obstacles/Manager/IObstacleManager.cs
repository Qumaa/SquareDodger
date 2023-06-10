using System.Collections.Generic;

namespace Project.Game
{
    public interface IObstacleManager : IUpdatable, IPausableAndResettable, IObstacleColorSource
    {
        IEnumerable<IObstacle> ActiveObstacles { get; }
    }
}