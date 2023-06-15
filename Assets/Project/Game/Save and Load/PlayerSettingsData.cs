using System;

namespace Project.Game
{
    [Serializable]
    public class PlayerSettingsData
    {
        private GameThemes _currentTheme;
        private bool _isCurrentThemeDark;

        public GameThemes CurrentTheme
        {
            get => _currentTheme;
            set
            {
                _currentTheme = value;
                InvokeThemeModified();
            }
        }

        public bool IsCurrentThemeDark
        {
            get => _isCurrentThemeDark;
            set
            {
                _isCurrentThemeDark = value;
                InvokeThemeModified();
            }
        }

        [field: NonSerialized] public event Action<GameThemes, bool> OnThemeModified;

        public PlayerSettingsData()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            CurrentTheme = GameThemes.Default;
            IsCurrentThemeDark = true;
        }

        private void InvokeThemeModified() =>
            OnThemeModified?.Invoke(CurrentTheme, IsCurrentThemeDark);
    }
}