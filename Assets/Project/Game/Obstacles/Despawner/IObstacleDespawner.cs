using UnityEngine;

namespace Project.Game
{
    public interface IObstacleDespawner
    {
        void DespawnNecessaryObstacles(IObstacle[] obstacles);
        void DespawnSingle(IObstacle obstacle);
    }
}