using System;

namespace Project.UI
{
    public interface IGameplayUI : IGameCanvasUI, IGameScoreDisplay
    {
        event Action OnPausePressed;
    }
}