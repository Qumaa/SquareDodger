using UnityEngine;
using UnityEngine.Audio;

namespace Project.Game
{
    public struct GameSoundsFactory : IFactory<IGameSounds>
    { 
        private GameSoundsRuntimeData _soundsData;
        public GameSoundsFactory(GameSoundsRuntimeData soundsData)
        {
            _soundsData = soundsData;
           
        }
        
        public IGameSounds CreateNew()
        {
            AudioSource audioSource = GameObject.Instantiate(_soundsData.AudioSourcePrefab).GetComponent<AudioSource>();
            AudioMixer masterMixer = _soundsData.MasterMixerGroup.audioMixer;
            AudioMixerGroup soundsMixer = _soundsData.SoundsMixerGroup;
            AudioMixerGroup musicMixer = _soundsData.MusicMixerGroup;
            GameSounds gameSounds = new GameSounds(masterMixer, audioSource,soundsMixer,musicMixer); 
            return gameSounds;
        }
    }
}