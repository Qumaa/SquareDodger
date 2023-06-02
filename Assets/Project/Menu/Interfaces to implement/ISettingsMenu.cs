using System;

namespace Project.UI
{
    public interface ISettingsMenu : IGameUI
    {
        event Action OnClosePressed;
    }
}