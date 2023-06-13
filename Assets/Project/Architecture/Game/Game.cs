using System.Collections.Generic;
using Project.Game;
using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public class Game : IGame
    {
        private IGameStateMachine _stateMachine;

        private GameThemeApplier _themeApplier;
        private GameRuntimeData _gameData;
        private Camera _camera;
        private IDisposer _disposer;

        private List<IUpdatable> _updatables;
        private List<IFixedUpdatable> _fixedUpdatables;

        public IGameplay Gameplay { get; set; }
        public IGameCanvasUIRenderer UI { get; set; }
        public ICameraController CameraController { get; set; }
        public IGameInputService InputService { get; set; }


        public Game(GameRuntimeData gameData, Camera camera, IDisposer disposer,
            IGameThemeResolver gameThemeResolver)
        {
            _themeApplier = new GameThemeApplier(gameThemeResolver);
            _updatables = new List<IUpdatable>();
            _fixedUpdatables = new List<IFixedUpdatable>();

            _gameData = gameData;
            _camera = camera;
            _disposer = disposer;

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

        public void ApplyTheme(GameThemes themeType, bool dark = true) =>
            _themeApplier.ApplyTheme(themeType, dark);

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
                _themeApplier,
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
    }
}