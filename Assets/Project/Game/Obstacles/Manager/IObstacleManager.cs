using System.Collections.Generic;

namespace Project.Game
{
    public interface IObstacleManager : IUpdatable, IPausableAndResettable, IGameThemeAppender
    {
        List<IObstacle> ActiveObstacles { get; }
    }
}