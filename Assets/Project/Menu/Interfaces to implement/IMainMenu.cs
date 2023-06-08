using System;

namespace Project.UI
{
    public interface IMainMenu : IGameCanvasUI, IOpenSettingsUI
    {
        event Action OnGameStartPressed;
        event Action OnApplicationQuitPressed;
    }
}