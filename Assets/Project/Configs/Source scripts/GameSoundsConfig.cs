using UnityEngine;
using UnityEngine.Audio;

namespace Project.Game
{
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
}