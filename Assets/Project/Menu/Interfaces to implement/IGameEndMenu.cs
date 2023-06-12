using System;
using Project.Game;

namespace Project.UI
{
    public interface IGameEndMenu : IGameCanvasUI, IOpenSettingsUI, IReturnToMenuUI
    {
        event Action OnRestartGamePressed;
    }
}