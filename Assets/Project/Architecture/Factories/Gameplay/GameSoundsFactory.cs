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
            var soundsAudioSource = CreateAudioSource(_soundsData.SoundsAudioSourcePrefab);
            var musicAudioSource = CreateAudioSource(_soundsData.MusicAudioSourcePrefab);
            
            var gameSounds = new GameSounds(
                _soundsData.MasterMixer, 
                soundsAudioSource, 
                musicAudioSource,
                _soundsData.TurnClip,
                _soundsData.InterfaceTapClip,
                _soundsData.LoseClip
                );
            
            return gameSounds;
        }

        private static AudioSource CreateAudioSource(GameObject prefab) =>
            GameObject.Instantiate(prefab).GetComponent<AudioSource>();
    }
}