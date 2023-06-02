using System;

namespace Project.UI
{
    public interface IPauseMenu : IGameUI, IOpenSettingsUI, IReturnToMenuUI
    {
        event Action OnContinuePressed;
    }
}