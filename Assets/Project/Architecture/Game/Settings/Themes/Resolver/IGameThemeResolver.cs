namespace Project.Game
{
    public interface IGameThemeResolver
    {
        IGameTheme Resolve(GameTheme themeType, bool dark = true);
    }
}