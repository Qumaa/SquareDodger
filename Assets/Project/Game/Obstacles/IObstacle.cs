using System;

namespace Project
{
    public interface IObstacle
    {
        event Action<IObstacle> OnDespawned;
    }
}