using System;

namespace Project.Game
{
    public interface IGameThemeObserver
    {
        void SetTheme(GameTheme themeType, bool dark);
        event Action<IGameTheme> OnThemeChanged;
    }
}