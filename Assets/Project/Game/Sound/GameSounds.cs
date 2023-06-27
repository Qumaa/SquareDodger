using UnityEngine;
using UnityEngine.Audio;

namespace Project.Game
{
    public class GameSounds : IGameSounds
    {
        public void PlayTurnSound()
        {
        }

        public void PlayLoseSound()
        {
        }

        public void PlayInterfaceTapSound()
        {
        }

        public void PlayMusicInLoop()
        {
        }

        public void StopMusic()
        {
        }

        public void SetMasterVolume(float volume)
        {
        }

        public void SetSoundsVolume(float volume)
        {
        }

        public void SetMusicVolume(float volume)
        {
        }
    }

    public interface IGameSounds
    {
        void PlayTurnSound();
        void PlayLoseSound();
        void PlayInterfaceTapSound();
        void PlayMusicInLoop();
        void StopMusic();
        void SetMasterVolume(float volume);
        void SetSoundsVolume(float volume);
        void SetMusicVolume(float volume);
    }

    [CreateAssetMenu(menuName = AssetMenuPaths.SOUND_CONFIG, fileName = "New Game Sounds Config")]
    public class GameSoundsConfig : ScriptableObject
    {
        [SerializeField] private AudioSource _audioSourcePrefab;
        [SerializeField] private AudioMixer _masterMixer;
        [SerializeField] private AudioMixer _soundsMixer;
        [SerializeField] private AudioMixer _musicMixer;

        public GameObject AudioSourcePrefab => _audioSourcePrefab.gameObject;
        public AudioMixer MasterMixer => _masterMixer;
        public AudioMixer SoundsMixer => _soundsMixer;
        public AudioMixer MusicMixer => _musicMixer;
    }

    public class GameSoundsRuntimeData : ILoadableFrom<GameSoundsConfig>
    {
        public GameObject AudioSourcePrefab { get; private set; }
        public AudioMixer MasterMixer { get; private set; }
        public AudioMixer SoundsMixer { get; private set; }
        public AudioMixer MusicMixer { get; private set; }

        public void Load(GameSoundsConfig data)
        {
            AudioSourcePrefab = data.AudioSourcePrefab;
            MasterMixer = data.MasterMixer;
            SoundsMixer = data.SoundsMixer;
            MusicMixer = data.MusicMixer;
        }
    }
}