using UnityEngine;
using UnityEngine.Audio;

namespace Project.Game
{
    public class GameSoundsRuntimeData : ILoadableFrom<GameSoundsConfig>
    {
        public GameObject SoundsAudioSourcePrefab { get; private set; }
        public GameObject MusicAudioSourcePrefab { get; private set; }
        public AudioMixer MasterMixer { get; private set; }
        public AudioClip TurnClip { get; private set; }
        public AudioClip InterfaceTapClip { get; private set; }
        public AudioClip LoseClip { get; private set; }

        public void Load(GameSoundsConfig data)
        {
            SoundsAudioSourcePrefab = data.SoundsAudioSourcePrefab;
            MusicAudioSourcePrefab = data.MusicAudioSourcePrefab;
            MasterMixer = data.MasterMixer;
            TurnClip = data.TurnClip;
            InterfaceTapClip = data.InterfaceTapClip;
            LoseClip = data.LoseClip;
        }
    }
}