using System;

namespace Project.Game
{
    public interface IGameSoundsVolumeObserver
    {
        void SetMasterVolume(float volume);
        event Action<float> OnMasterVolumeChanged;
        
        void SetSoundsVolume(float volume);
        event Action<float> OnSoundsVolumeChanged;
        
        void SetMusicVolume(float volume);
        event Action<float> OnMusicVolumeChanged;
    }
}