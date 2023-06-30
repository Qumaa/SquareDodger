using System;

namespace Project.Game
{
    public interface IGameLocaleObserver
    {
        void SetLocale(GameLocale locale);
        event Action<GameLocale> OnLocaleChanged;
    }
}