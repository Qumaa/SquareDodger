using Project.Architecture;
using Project.UI;

namespace Project.Game
{
    public class PlayerSettingsMaintainer : IPlayerSettingsMaintainer
    {
        private PlayerSettingsData _data;
        private ISavingSystem<PlayerSettingsData> _savingSystem;

        public PlayerSettingsMaintainer(ISavingSystem<PlayerSettingsData> savingSystem, ISettingsMenu settings, IGame game)
        {
            _data = savingSystem.LoadData();
            _savingSystem = savingSystem;
            
            settings.SetSettingsData(_data);
            settings.OnClosePressed += SaveOnClose;

            game.ApplyTheme(_data.CurrentTheme, _data.IsCurrentThemeDark);
            _data.OnThemeModified += game.ApplyTheme;
        }

        private void SaveOnClose()
        {
            _savingSystem.SaveData(_data);
        }
    }
}