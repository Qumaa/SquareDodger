using System;

namespace Project.Game
{
    public interface IPlayerCollisionDetector
    {
        event Action OnCollided;
    }
}