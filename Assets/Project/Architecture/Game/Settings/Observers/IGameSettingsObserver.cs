namespace Project.Game
{
    public interface IGameSettingsObserver : IGameThemeObserver, IGameShaderModeObserver, IGameLocaleObserver,
        IGameSoundsVolumeObserver
    {
        
    }
}