using System;

namespace Project.Game
{
    public interface IGameplay : IPausableAndResettable, IUpdatableAnFixedUpdatable
    {
        event Action OnEnded;
    }
}