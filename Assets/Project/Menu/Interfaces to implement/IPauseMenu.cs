using System;

namespace Project.UI
{
    public interface IPauseMenu : IGameCanvasUI, IOpenSettingsUI, IReturnToMenuUI
    {
        event Action OnContinuePressed;
    }
}