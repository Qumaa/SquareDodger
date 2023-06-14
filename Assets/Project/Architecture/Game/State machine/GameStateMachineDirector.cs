using Project.Game;
using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public struct GameStateMachineDirector : IGameStateMachineDirector
    {
        private IGameStateMachine _stateMachine;
        private IGame _game;
        private GameRuntimeData _gameData;
        private IDisposer _disposer;
        private Camera _camera;
        private IGameThemeApplierComposite _themeApplier;
        private ISettingsMenuOpener _settingsOpener;
        private IApplicationQuitter _applicationQuitter;

        public GameStateMachineDirector(IGameStateMachine stateMachine, IGame game, GameRuntimeData gameData,
            IDisposer disposer, Camera camera, IGameThemeApplierComposite themeApplier,
            ISettingsMenuOpener settingsOpener, IApplicationQuitter applicationQuitter)
        {
            _stateMachine = stateMachine;
            _game = game;
            _gameData = gameData;
            _disposer = disposer;
            _camera = camera;
            _themeApplier = themeApplier;
            _settingsOpener = settingsOpener;
            _applicationQuitter = applicationQuitter;
        }

        public void Build(IGameStateMachine machine)
        {
            var bootstrap = new BootstrapState(_stateMachine, _game, _disposer, _gameData, _camera, _themeApplier);
            var initializeMenu = new InitializeUIState(_stateMachine, _game, _gameData.GameUIData, _settingsOpener);
            var menuState = new MenuState(_stateMachine, _game, _settingsOpener, _applicationQuitter);
            var gameLoop = new GameLoopState(_stateMachine, _game);
            var gamePaused = new GamePauseState(_stateMachine, _game, _settingsOpener);
            var gameEnd = new GameEndState(_stateMachine, _game, _settingsOpener);
            var gameRestart = new RestartGameState(_stateMachine, _game);

            _stateMachine.AddState(bootstrap)
                .AddState(initializeMenu)
                .AddState(menuState)
                .AddState(gameLoop)
                .AddState(gamePaused)
                .AddState(gameEnd)
                .AddState(gameRestart);
        }
    }
}