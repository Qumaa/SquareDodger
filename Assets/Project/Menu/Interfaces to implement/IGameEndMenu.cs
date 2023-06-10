using System;
using Project.Game;

namespace Project.UI
{
    public interface IGameEndMenu : IGameCanvasUI, IOpenSettingsUI, IReturnToMenuUI, IGameInputServiceConsumer
    {
        event Action OnRestartGamePressed;
    }
}