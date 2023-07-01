using UnityEngine;
using UnityEngine.Audio;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.SOUND_CONFIG, fileName = "New Game Sounds Config")]
    public class GameSoundsConfig : ScriptableObject
    {
        [SerializeField] private AudioSource _audioSourcePrefab;
        [SerializeField] private AudioMixerGroup  _masterMixer;
        [SerializeField] private AudioMixerGroup _soundsMixer;
        [SerializeField] private AudioMixerGroup _musicMixer;

        public GameObject AudioSourcePrefab => _audioSourcePrefab.gameObject;
        public AudioMixerGroup MasterMixer => _masterMixer;
        public AudioMixerGroup SoundsMixer => _soundsMixer;
        public AudioMixerGroup MusicMixer => _musicMixer;
     
    
    }
    
}