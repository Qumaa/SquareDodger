using System;
using Project.Game;

namespace Project.UI
{
    public interface IMainMenu : IGameCanvasUI, IOpenSettingsUI
    {
        event Action OnGameStartPressed;
        event Action OnApplicationQuitPressed;
    }
}