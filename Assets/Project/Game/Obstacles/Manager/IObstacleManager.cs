using System.Collections.Generic;

namespace Project.Game
{
    public interface IObstacleManager : IUpdatable, IPausableAndResettable, IObstacleColorSource
    {
        List<IObstacle> ActiveObstacles { get; }
    }
}