using System;
using Project.Game;

namespace Project
{
    public class Obstacle : IObstacle, IPoolerTarget
    {
        public event Action<IObstacle> OnDespawned;
        public void ResetToDefault()
        {
            throw new NotImplementedException();
        }

        public void Pool()
        {
            OnDespawned?.Invoke(this);
        }
    }
}