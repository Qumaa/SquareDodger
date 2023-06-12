using Project.Game;

namespace Project.Architecture
{
    public interface IGameThemeResolver
    {
        IGameTheme Resolve(GameThemes themeType, bool dark = true);
    }
}