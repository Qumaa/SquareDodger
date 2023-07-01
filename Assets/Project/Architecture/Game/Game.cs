using System;
using System.Collections.Generic;
using Project.Game;
using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public class Game : IGame
    {
        private IGameStateMachine _stateMachine;

        private GameRuntimeData _gameData;
        private Camera _camera;
        private IDisposer _disposer;
        private readonly IGameThemeResolver _themeResolver;

        private List<IUpdatable> _updatables;
        private List<IFixedUpdatable> _fixedUpdatables;

        public IGameplay Gameplay { get; set; }
        public IGameCanvasUIRenderer UI { get; set; }
        public ICameraController CameraController { get; set; }
        public IGameInputService InputService { get; set; }
        public IGameSounds GameSounds { get; set; }

        public event Action<IGameTheme> OnThemeChanged;
        public event Action<ShaderBlendingMode> OnPlayerShaderModeChanged;
        public event Action<GameLocale> OnLocaleChanged;
        public event Action<float> OnMasterVolumeChanged;
        public event Action<float> OnSoundsVolumeChanged;
        public event Action<float> OnMusicVolumeChanged;

        public Game(GameRuntimeData gameData, Camera camera, IDisposer disposer,
            IGameThemeResolver themeResolver)
        {
            _updatables = new List<IUpdatable>();
            _fixedUpdatables = new List<IFixedUpdatable>();

            _gameData = gameData;
            _camera = camera;
            _disposer = disposer;
            _themeResolver = themeResolver;

            InitializeStateMachine();
            InitializeStates();
        }

        public void Update(float timeStep)
        {
            foreach(var updatable in _updatables)
                updatable.Update(timeStep);
        }

        public void FixedUpdate(float fixedTimeStep)
        {
            foreach(var fixedUpdatable in _fixedUpdatables)
                fixedUpdatable.FixedUpdate(fixedTimeStep);
        }

        public void Initialize()
        {
            _stateMachine.SetState<BootstrapState>();
            
            _updatables.Add(Gameplay);
            _fixedUpdatables.Add(Gameplay);
        }

        private void InitializeStateMachine()
        {
            _stateMachine = new GameStateMachine();
        }

        private void InitializeStates()
        {
            var settingsOpener = new SettingsMenuOpenerFactory().CreateNew();
            var quitter = new ApplicationQuitterFactory().CreateNew();
            
            var director = new GameStateMachineDirector(
                _stateMachine,
                this,
                _gameData,
                _disposer,
                _camera,
                settingsOpener,
                quitter
                );
            
            director.Build(_stateMachine);
        }

        public void Add(IUpdatable item) =>
            _updatables.Add(item);

        public void Remove(IUpdatable item) =>
            _updatables.Remove(item);

        public void Add(IFixedUpdatable item) =>
            _fixedUpdatables.Add(item);

        public void Remove(IFixedUpdatable item) =>
            _fixedUpdatables.Remove(item);

        public void SetTheme(GameTheme themeType, bool dark = true) =>
            OnThemeChanged?.Invoke(_themeResolver.Resolve(themeType, dark));

        public void SetPlayerShaderMode(ShaderBlendingMode mode) =>
            OnPlayerShaderModeChanged?.Invoke(mode);

        public void SetLocale(GameLocale locale) =>
            OnLocaleChanged?.Invoke(locale);

        public void SetMasterVolume(float volume) =>
            OnMasterVolumeChanged?.Invoke(volume);

        public void SetSoundsVolume(float volume) =>
            OnSoundsVolumeChanged?.Invoke(volume);

        public void SetMusicVolume(float volume) =>
            OnMusicVolumeChanged?.Invoke(volume);
    }
}