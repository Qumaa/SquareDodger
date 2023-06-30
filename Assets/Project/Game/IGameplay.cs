using System;

namespace Project.Game
{
    public interface IGameplay : IPausableAndResettable, IUpdatableAnFixedUpdatable, IScoreSource
    {
        event Action OnEnded;
    }
}