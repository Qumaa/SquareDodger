namespace Project.Game
{
    public interface IGameThemeApplier
    {
        void ApplyTheme(GameTheme themeType, bool dark = true);
    }
}