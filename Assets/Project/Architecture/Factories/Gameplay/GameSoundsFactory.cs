using UnityEngine;

namespace Project.Game
{
    public struct GameSoundsFactory : IFactory<IGameSounds>
    {
        private GameSoundsRuntimeData _soundsData;
        private IGameSoundsVolumeObserver _soundsObserver;

        public GameSoundsFactory(GameSoundsRuntimeData soundsData, IGameSoundsVolumeObserver soundsObserver)
        {
            _soundsData = soundsData;
            _soundsObserver = soundsObserver;
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

            SubscribeObserverEvents(gameSounds);
            
            return gameSounds;
        }

        private void SubscribeObserverEvents(IGameSounds sounds)
        {
            _soundsObserver.OnMasterVolumeChanged += sounds.SetMasterVolume;
            _soundsObserver.OnSoundsVolumeChanged += sounds.SetSoundsVolume;
            _soundsObserver.OnMusicVolumeChanged += sounds.SetMusicVolume;
        }

        private static AudioSource CreateAudioSource(GameObject prefab) =>
            GameObject.Instantiate(prefab).GetComponent<AudioSource>();
    }
}