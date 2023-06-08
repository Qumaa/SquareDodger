using System;

namespace Project.UI
{
    public interface ISettingsMenu : IGameCanvasUI
    {
        event Action OnClosePressed;
    }
}