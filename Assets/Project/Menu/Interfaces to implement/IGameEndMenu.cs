using System;

namespace Project.UI
{
    public interface IGameEndMenu : IGameCanvasUI, IOpenSettingsUI, IReturnToMenuUI
    {
        event Action OnRestartGamePressed;
    }
}