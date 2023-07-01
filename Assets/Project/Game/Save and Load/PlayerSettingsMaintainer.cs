using Project.Architecture;
using Project.UI;

namespace Project.Game
{
    public class PlayerSettingsMaintainer
    {
        private readonly PlayerSettingsData _data;
        private readonly ISavingSystem<PlayerSettingsData> _savingSystem;

        public PlayerSettingsMaintainer(ISavingSystem<PlayerSettingsData> savingSystem, ISettingsMenu settings,
            IGame game)
        {
            _data = savingSystem.LoadData();
            _savingSystem = savingSystem;

            settings.SetSettingsData(_data);
            settings.OnClosePressed += SaveOnClose;

            game.SetTheme(_data.CurrentTheme, _data.IsCurrentThemeDark);
            _data.OnThemeModified += game.SetTheme;

            game.SetPlayerShaderMode(_data.ShaderMode);
            _data.OnShaderModeModified += game.SetPlayerShaderMode;

            game.SetLocale(_data.GameLocale);
            _data.OnGameLocaleModified += game.SetLocale;
            
            game.SetMasterVolume(_data.MasterVolume);
            game.SetSoundsVolume(_data.SoundsVolume);
            game.SetMusicVolume(_data.MusicVolume);
            _data.OnMasterVolumeChanged += game.SetMasterVolume;
            _data.OnSoundsVolumeChanged += game.SetSoundsVolume;
            _data.OnMusicVolumeChanged += game.SetMusicVolume;
        }

        private void SaveOnClose()
        {
            _savingSystem.SaveData(_data);
        }
    }
}