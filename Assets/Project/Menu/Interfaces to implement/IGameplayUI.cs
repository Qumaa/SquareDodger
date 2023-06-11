using System;

namespace Project.UI
{
    public interface IGameplayUI : IGameCanvasUI, IGameScoreDisplayer
    {
        event Action OnPausePressed;
    }
}