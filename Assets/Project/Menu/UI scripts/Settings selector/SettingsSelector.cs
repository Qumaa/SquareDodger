using Project.Game;

namespace Project.UI
{
    public abstract class SettingsSelector : ISettingsSelector
    {
        private PlayerSettingsData _data;
        
        public void SetData(PlayerSettingsData data)
        {
            UnsubscribeEventsFromData(_data);
            _data = data;
            SubscribeEventsToData(data);
            UpdateUIToMatchData(data);
        }

        protected abstract void UnsubscribeEventsFromData(PlayerSettingsData data);
        protected abstract void SubscribeEventsToData(PlayerSettingsData data);
        protected abstract void UpdateUIToMatchData(PlayerSettingsData data);

        protected void SetThemeType(GameTheme theme) =>
            _data.CurrentTheme = theme;
        
        protected void SetThemeMode(bool dark) =>
            _data.IsCurrentThemeDark = dark;

        protected void SetShaderMode(ShaderBlendingMode mode) =>
            _data.ShaderMode = mode;
    }
}