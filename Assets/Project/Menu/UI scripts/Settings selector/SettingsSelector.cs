using Project.Game;

namespace Project.UI
{
    public abstract class SettingsSelector : ISettingsSelector
    {
        private PlayerSettingsData _settings;
        
        public void SetData(PlayerSettingsData data)
        {
            UnsubscribeEventsFromData();
            _settings = data;
            UpdateUIToMatchData(data);
            SubscribeEventsToData();
        }

        protected abstract void UnsubscribeEventsFromData();
        protected abstract void SubscribeEventsToData();
        protected abstract void UpdateUIToMatchData(PlayerSettingsData data);

        protected void SetMasterVolume(float volume) =>
            _settings.MasterVolume = volume;

        protected void SetSoundsVolume(float volume) =>
            _settings.SoundsVolume = volume;

        protected void SetMusicVolume(float volume) =>
            _settings.MusicVolume = volume;
        
        protected void SetThemeType(GameTheme theme) =>
            _settings.CurrentTheme = theme;
        
        protected void SetThemeMode(bool dark) =>
            _settings.IsCurrentThemeDark = dark;

        protected void SetShaderMode(ShaderBlendingMode mode) =>
            _settings.ShaderMode = mode;

        protected void SetLocale(GameLocale locale) =>
            _settings.GameLocale = locale;
    }
}