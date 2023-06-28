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

        public IGameTheme Resolve(GameTheme themeType, bool dark = true)
        {
            var themePath = ThemeEnumToResourcesPath(themeType, dark);
            
            var colors = Resources.Load<GameColorsConfig>(themePath);
            
            _theme.Load(colors);
            
            Resources.UnloadAsset(colors);

            return _theme;
        }

        private static string ThemeEnumToResourcesPath(GameTheme themeType, bool dark) =>
            GetPathPrefix(dark) + SwitchThemeType(themeType);

        private static string GetPathPrefix(bool dark) =>
            dark ? ResourcesPaths.Themes.DARK_THEMES : ResourcesPaths.Themes.BRIGHT_THEMES;

        private static string SwitchThemeType(GameTheme themeType) =>
            themeType switch
            {
                GameTheme.Default => ResourcesPaths.Themes.DEFAULT,
                GameTheme.Hot => ResourcesPaths.Themes.HOT,
                GameTheme.Hard => ResourcesPaths.Themes.HARD,
                _ => throw new ArgumentException()
            };

    }
}