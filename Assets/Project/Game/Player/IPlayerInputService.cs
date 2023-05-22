using System;

namespace Project.Game
{
    internal interface IPlayerInputService
    {
        event Action OnTurnInput;
    }
}