using System;
using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class ResourcesGameThemeResolver : IGameThemeResolver
    {
        private GameColorsRuntimeData _theme;

        public ResourcesGameThemeResolver()
        {
            _theme = new GameColorsRuntimeData();
        }

        public IGameTheme Resolve(GameThemes themeType, bool dark = true)
        {
            var themePath = ThemeEnumToResourcesPath(themeType, dark);
            
            var colors = Resources.Load<GameColorsConfig>(themePath);
            
            _theme.Load(colors);
            
            Resources.UnloadAsset(colors);

            return _theme;
        }

        private static string ThemeEnumToResourcesPath(GameThemes themeType, bool dark) =>
            GetPathPrefix(dark) + SwitchThemeType(themeType);

        private static string GetPathPrefix(bool dark) =>
            dark ? ResourcesPaths.Themes.DARK_THEMES : ResourcesPaths.Themes.BRIGHT_THEMES;

        private static string SwitchThemeType(GameThemes themeType) =>
            themeType switch
            {
                GameThemes.Default => ResourcesPaths.Themes.DEFAULT,
                GameThemes.Hot => ResourcesPaths.Themes.HOT,
                GameThemes.Hard => ResourcesPaths.Themes.HARD,
                _ => throw new ArgumentException()
            };

    }
}