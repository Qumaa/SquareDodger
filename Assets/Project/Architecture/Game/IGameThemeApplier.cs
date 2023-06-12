using Project.Architecture;

namespace Project.Game
{
    public interface IGameThemeApplier
    {
        void ApplyTheme(GameThemes themeType, bool dark = true);
    }
}