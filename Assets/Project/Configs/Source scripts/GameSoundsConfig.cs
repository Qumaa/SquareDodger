using UnityEngine;
using UnityEngine.Audio;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.SOUND_CONFIG, fileName = "New Game Sounds Config")]
    public class GameSoundsConfig : ScriptableObject
    {
        [SerializeField] private AudioSource _soundsAudioSourcePrefab;
        [SerializeField] private AudioSource _musicAudioSourcePrefab;
        [SerializeField] private AudioMixer _masterMixer;
        [SerializeField] private AudioClip _turnClip;
        [SerializeField] private AudioClip _interfaceTapClip;
        [SerializeField] private AudioClip _loseClip;

        public GameObject SoundsAudioSourcePrefab => _soundsAudioSourcePrefab.gameObject;
        public GameObject MusicAudioSourcePrefab => _musicAudioSourcePrefab.gameObject;
        public AudioMixer MasterMixer => _masterMixer;
        public AudioClip TurnClip => _turnClip;
        public AudioClip InterfaceTapClip => _interfaceTapClip;
        public AudioClip LoseClip => _loseClip;
    }
}