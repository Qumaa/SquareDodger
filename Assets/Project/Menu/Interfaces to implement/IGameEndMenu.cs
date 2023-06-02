using System;

namespace Project.UI
{
    public interface IGameEndMenu : IGameUI, IOpenSettingsUI, IReturnToMenuUI
    {
        event Action OnRestartGamePressed;
    }
}