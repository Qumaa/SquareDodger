using System.Collections.Generic;

namespace Project.Game
{
    public class GameThemeApplier : IGameThemeAppenderComposite
    {
        private List<IGameThemeAppender> _appenders;

        public GameThemeApplier()
        {
            _appenders = new List<IGameThemeAppender>();
        }

        public void ApplyTheme(IGameTheme theme)
        {
            foreach(var appender in _appenders)
                appender.ApplyTheme(theme);
        }

        public void Add(IGameThemeAppender item)
        {
            _appenders.Add(item);
        }

        public void Remove(IGameThemeAppender item)
        {
            _appenders.Remove(item);
        }
    }
}