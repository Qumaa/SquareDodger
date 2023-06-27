using UnityEngine;
using UnityEngine.Audio;

namespace Project.Game
{
    public class GameSounds : IGameSounds
    {
        private const string _MASTER_VOLUME = "";
        private const string _MUSIC_VOLUME = "";
        private const string _SOUNDS_VOLUME = "";

        private AudioMixer _masterMixer;
        
        public void PlayTurnSound()
        {
            Debug.Log("turn");
        }

        public void PlayLoseSound()
        {
            Debug.Log("lose");
        }

        public void PlayInterfaceTapSound()
        {
            Debug.Log("interface tap");
        }

        public void PlayMusicInLoop()
        {
            Debug.Log("music starts");
        }

        public void StopMusic()
        {
        }

        public void SetMasterVolume(float volume) =>
            _masterMixer.SetFloat(_MASTER_VOLUME, InputToVolume(volume));

        public void SetSoundsVolume(float volume) =>
            _masterMixer.SetFloat(_SOUNDS_VOLUME, InputToVolume(volume));

        public void SetMusicVolume(float volume) =>
            _masterMixer.SetFloat(_MUSIC_VOLUME, InputToVolume(volume));

        private static float InputToVolume(float inputValue) =>
            inputValue == 0 ? 0 : Mathf.Log10(inputValue) * 20;
    }
}