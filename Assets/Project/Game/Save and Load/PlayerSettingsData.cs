using System;

namespace Project.Game
{
    [Serializable]
    public class PlayerSettingsData
    {
        private GameTheme _currentTheme;
        private bool _isCurrentThemeDark;

        private ShaderBlendingMode _shaderMode;

        public GameTheme CurrentTheme
        {
            get => _currentTheme;
            set => SetCurrentTheme(value);
        }

        public bool IsCurrentThemeDark
        {
            get => _isCurrentThemeDark;
            set => SetCurrentThemeMode(value);
        }

        public ShaderBlendingMode ShaderMode
        {
            get => _shaderMode;
            set => SetShaderMode(value);
        }

        [field: NonSerialized] public event Action<GameTheme, bool> OnThemeModified;
        [field: NonSerialized] public event Action<ShaderBlendingMode> OnShaderModeModified;

        public PlayerSettingsData()
        {
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            CurrentTheme = GameTheme.Default;
            IsCurrentThemeDark = true;
            ShaderMode = ShaderBlendingMode.None;
        }

        private void SetCurrentTheme(GameTheme theme)
        {
            if (theme == _currentTheme)
                return;

            _currentTheme = theme;
            InvokeThemeModified();
        }

        private void SetCurrentThemeMode(bool dark)
        {
            if (dark == _isCurrentThemeDark)
                return;

            _isCurrentThemeDark = dark;
            InvokeThemeModified();
        }

        private void SetShaderMode(ShaderBlendingMode mode)
        {
            if (mode == _shaderMode)
                return;

            _shaderMode = mode;
            OnShaderModeModified?.Invoke(_shaderMode);
        }

        private void InvokeThemeModified() =>
            OnThemeModified?.Invoke(CurrentTheme, IsCurrentThemeDark);
    }
}