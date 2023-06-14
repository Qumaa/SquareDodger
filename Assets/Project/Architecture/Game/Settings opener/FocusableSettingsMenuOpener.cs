namespace Project.UI
{
    public class FocusableSettingsMenuOpener : ISettingsMenuOpener
    {
        private ISettingsMenu _settingsMenu;
        
        public IGameCanvasUIFocuser Focuser { get; set; }
        public ISettingsMenu SettingsMenu
        {
            get => _settingsMenu;
            set => SetSettingMenu(value);
        }

        public void OpenSettings()
        {
            _settingsMenu.Show();
            Focuser.SetFocus(_settingsMenu);
        }

        private void CloseSettings()
        {
            _settingsMenu.Hide();
            Focuser.Unfocus();
        }

        private void SetSettingMenu(ISettingsMenu menu)
        {
            if (_settingsMenu != null)
                _settingsMenu.OnClosePressed -= CloseSettings;
                
            _settingsMenu = menu;
            _settingsMenu.OnClosePressed += CloseSettings;
        }
    }
}