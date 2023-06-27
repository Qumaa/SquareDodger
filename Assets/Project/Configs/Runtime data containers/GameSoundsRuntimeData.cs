using UnityEngine;
using UnityEngine.Audio;

namespace Project.Game
{
    public class GameSoundsRuntimeData : ILoadableFrom<GameSoundsConfig>
    {
        public GameObject AudioSourcePrefab { get; private set; }
        public AudioMixer MasterMixer { get; private set; }
        public AudioMixer SoundsMixer { get; private set; }
        public AudioMixer MusicMixer { get; private set; }

        public void Load(GameSoundsConfig data)
        {
            // AudioSourcePrefab = data.AudioSourcePrefab;
            // MasterMixer = data.MasterMixer;
            // SoundsMixer = data.SoundsMixer;
            // MusicMixer = data.MusicMixer;
        }
    }
}