using System;
using Project.Game;

namespace Project.UI
{
    public interface IPauseMenu : IGameCanvasUI, IOpenSettingsUI, IReturnToMenuUI, IGameInputServiceConsumer
    {
        event Action OnContinuePressed;
    }
}