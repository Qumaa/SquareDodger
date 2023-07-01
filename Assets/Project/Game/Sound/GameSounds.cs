using UnityEngine;
using UnityEngine.Audio;

namespace Project.Game
{
    public class GameSounds : IGameSounds
    {
        private const string _MASTER_VOLUME = "MasterVolume";
        private const string _MUSIC_VOLUME = "MusicVolume";
        private const string _SOUNDS_VOLUME = "SoundVolume";

        private readonly AudioMixer _masterMixer;
        private readonly AudioSource _soundsSource;
        private readonly AudioSource _musicSource;

        private readonly AudioClip _turnSound;
        private readonly AudioClip _interfaceTapSound;
        private readonly AudioClip _loseSound;

        public GameSounds(AudioMixer masterMixer, AudioSource soundsSource, AudioSource musicSource,
            AudioClip turnSound, AudioClip interfaceTapSound, AudioClip loseSound)
        {
            _masterMixer = masterMixer;
            _soundsSource = soundsSource;
            _musicSource = musicSource;

            _turnSound = turnSound;
            _interfaceTapSound = interfaceTapSound;
            _loseSound = loseSound;
        }

        public void PlayTurnSound()
        {
            Debug.Log("turn");
            PlayClip(_turnSound);
        }

        public void PlayLoseSound()
        {
            Debug.Log("lose");
            PlayClip(_loseSound);
        }

        public void PlayInterfaceTapSound()
        {
            Debug.Log("interface tap");
            PlayClip(_interfaceTapSound);
        }

        public void PlayMusicInLoop()
        {
            Debug.Log("music starts");
            _musicSource.Play();
        }

        public void StopMusic()
        {
        }

        private void PlayClip(AudioClip clip) =>
            _soundsSource.PlayOneShot(clip);

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