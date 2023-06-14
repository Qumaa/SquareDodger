namespace Project.UI
{
    public interface ISettingsMenuOpener
    {
        IGameCanvasUIFocuser Focuser { get; set; }
        ISettingsMenu SettingsMenu { get; set; }
        void OpenSettings();
    }
}