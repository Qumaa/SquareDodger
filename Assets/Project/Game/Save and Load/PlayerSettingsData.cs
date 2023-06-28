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

        public ShaderBlendingMode ShaderMode
        {
            get => _shaderMode;
            set
            {
                _shaderMode = value;
                OnShaderModeModified?.Invoke(_shaderMode);
            }
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

        private void InvokeThemeModified() =>
            OnThemeModified?.Invoke(CurrentTheme, IsCurrentThemeDark);
    }
}