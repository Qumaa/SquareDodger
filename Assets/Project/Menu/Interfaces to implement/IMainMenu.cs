using System;
using Project.Game;

namespace Project.UI
{
    public interface IMainMenu : IGameCanvasUI, IOpenSettingsUI, IGameInputServiceConsumer
    {
        event Action OnGameStartPressed;
        event Action OnApplicationQuitPressed;
    }
}