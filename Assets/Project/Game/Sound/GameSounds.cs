using UnityEngine;
using UnityEngine.Audio;

namespace Project.Game
{
    public class GameSounds : IGameSounds
    {
        private const string _MASTER_VOLUME = "MasterVolume";
        private const string _MUSIC_VOLUME = "MusicVolume";
        private const string _SOUNDS_VOLUME = "SoundVolume";

        private AudioMixer _masterMixer;
        private AudioSource _audioSource;
        private AudioMixerGroup _soundsMixer;
        private AudioMixerGroup _musicMixer;
       

        public GameSounds(AudioMixer masterMixer, AudioSource audioSource,AudioMixerGroup soundsMixer,AudioMixerGroup musicMixer)
        {
            _masterMixer = masterMixer;
            _audioSource = audioSource;
            _soundsMixer = soundsMixer;
            _musicMixer = musicMixer;
        }
        


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
            _audioSource.Play();

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
            inputValue == 0 ? -80 : Mathf.Log10(inputValue) * 20;
    }
}