using System;

namespace Project.UI
{
    public interface IGameEndMenu : IGameCanvasUI, IOpenSettingsUI, IReturnToMenuUI, IGameScoreDisplay
    {
        event Action OnRestartGamePressed;
    }
}