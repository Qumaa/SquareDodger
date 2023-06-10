using System;

namespace Project.UI
{
    public interface IMainMenu : IGameInputCanvasUI, IOpenSettingsUI
    {
        event Action OnGameStartPressed;
        event Action OnApplicationQuitPressed;
    }
}