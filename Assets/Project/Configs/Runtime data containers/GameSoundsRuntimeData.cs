using UnityEngine;
using UnityEngine.Audio;

namespace Project.Game
{
    public class GameSoundsRuntimeData : ILoadableFrom<GameSoundsConfig>
    {
    

        public GameObject AudioSourcePrefab { get; private set; }
        public AudioMixerGroup MasterMixerGroup { get; private set; }
        public AudioMixerGroup SoundsMixerGroup { get; private set; }
        public AudioMixerGroup MusicMixerGroup { get; private set; }
  

        public void Load(GameSoundsConfig data)
        {
            AudioSourcePrefab = data.AudioSourcePrefab;
            MasterMixerGroup = data.MasterMixer; 
            SoundsMixerGroup = data.SoundsMixer; 
            MusicMixerGroup = data.MusicMixer;
            
        }
       
    }
}