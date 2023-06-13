namespace Project.Game
{
    public interface IGameThemeResolver
    {
        IGameTheme Resolve(GameThemes themeType, bool dark = true);
    }
}