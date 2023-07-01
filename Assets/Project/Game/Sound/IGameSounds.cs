namespace Project.Game
{
    public interface IGameSounds
    {
        void PlayTurnSound();
        void PlayLoseSound();
        void PlayInterfaceTapSound();
        void PlayMusicInLoop();
        void SetMasterVolume(float volume);
        void SetSoundsVolume(float volume);
        void SetMusicVolume(float volume);
    }
}