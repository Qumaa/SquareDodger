using System;

namespace Project.UI
{
    public interface IMainMenu : IGameUI, IOpenSettingsUI
    {
        event Action OnGameStartPressed;
        event Action OnApplicationQuitPressed;
    }
}