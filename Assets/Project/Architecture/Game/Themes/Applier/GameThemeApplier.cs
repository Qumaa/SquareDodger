﻿using System.Collections.Generic;

namespace Project.Game
{
    public class GameThemeApplier : IGameThemeApplierComposite
    {
        private List<IGameThemeAppender> _appenders;
        private IGameThemeResolver _themeResolver;
        private GameTheme? _currentTheme;
        private bool _currentThemeMode;

        public GameThemeApplier(IGameThemeResolver themeResolver)
        {
            _appenders = new List<IGameThemeAppender>();
            _themeResolver = themeResolver;
        }

        public void Add(IGameThemeAppender item)
        {
            _appenders.Add(item);
        }

        public void Remove(IGameThemeAppender item)
        {
            _appenders.Remove(item);
        }

        public void ApplyTheme(GameTheme themeType, bool dark = true)
        {
            if (!ShouldApply(themeType, dark))
                return;
            
            ApplyThemeInternal(_themeResolver.Resolve(themeType, dark));
        }

        private void ApplyThemeInternal(IGameTheme theme)
        {
            foreach(var appender in _appenders)
                appender.ApplyTheme(theme);
        }
        
        private bool ShouldApply(GameTheme theme, bool mode)
        {
            var result = _currentTheme == null || _currentTheme != theme || _currentThemeMode != mode;
            _currentTheme = theme;
            _currentThemeMode = mode;
            return result;
        }
    }
}