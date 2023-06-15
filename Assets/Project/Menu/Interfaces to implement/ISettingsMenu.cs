using System;
using Project.Game;

namespace Project.UI
{
    public interface ISettingsMenu : IGameCanvasUI
    {
        event Action OnClosePressed;
        void SetSettingsData(PlayerSettingsData data);
    }
}