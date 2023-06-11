using System;
using System.Collections.Generic;

namespace Project.Game
{
    public interface IObstacleDespawner
    {
        event Action<IObstacle> OnDespawned;
        void DespawnNecessaryObstacles(List<IObstacle> obstacles);
        void DespawnSingle(IObstacle obstacle);
    }
}