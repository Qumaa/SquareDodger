using System;
using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class ResourcesGameThemeResolver : IGameThemeResolver
    {
        public IGameTheme Resolve(GameThemes themeType, bool dark = true)
        {
            var themePath = ThemeEnumToResourcesPath(themeType, dark);
            
            var colors = Resources.Load<GameColorsConfig>(themePath);
            
            var theme = new GameColorsRuntimeData();
            theme.Load(colors);
            
            Resources.UnloadAsset(colors);

            return theme;
        }

        private static string ThemeEnumToResourcesPath(GameThemes themeType, bool dark) =>
            GetPathPrefix(dark) + SwitchThemeType(themeType);

        private static string GetPathPrefix(bool dark) =>
            dark ? ResourcesPaths.Themes.DARK_THEMES : ResourcesPaths.Themes.BRIGHT_THEMES;

        public static string SwitchThemeType(GameThemes themeType) =>
            themeType switch
            {
                GameThemes.Default => ResourcesPaths.Themes.DEFAULT,
                GameThemes.HOT => ResourcesPaths.Themes.HOT,
                GameThemes.HARD => ResourcesPaths.Themes.HARD,
                _ => throw new ArgumentException()
            };

    }
}