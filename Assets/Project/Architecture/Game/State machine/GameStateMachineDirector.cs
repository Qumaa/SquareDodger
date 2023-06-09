﻿using Project.Game;
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
        private ISettingsMenuOpener _settingsOpener;
        private IApplicationQuitter _applicationQuitter;
        private PlayerSettingsSavingSystem _settingsSavingSystem;
        private PlayerProgressSavingSystem _progressSavingSystem;

        public GameStateMachineDirector(IGameStateMachine stateMachine, IGame game, GameRuntimeData gameData,
            IDisposer disposer, Camera camera,
            ISettingsMenuOpener settingsOpener, IApplicationQuitter applicationQuitter)
        {
            _stateMachine = stateMachine;
            _game = game;
            _gameData = gameData;
            _disposer = disposer;
            _camera = camera;
            _settingsOpener = settingsOpener;
            _applicationQuitter = applicationQuitter;
            _settingsSavingSystem = new PlayerSettingsSavingSystem();
            _progressSavingSystem = new PlayerProgressSavingSystem();
        }

        public void Build(IGameStateMachine machine)
        {
            var bootstrap = 
                new BootstrapState(_stateMachine, _game, _disposer, _gameData, _camera);
            var initializeMenu = 
                new InitializeUIState(_stateMachine, _game, _gameData.GameUIData, _settingsOpener, _settingsSavingSystem);
            var menuState = 
                new MenuState(_stateMachine, _game, _settingsOpener, _applicationQuitter);
            var gameLoop = 
                new GameLoopState(_stateMachine, _game, _progressSavingSystem);
            var gamePaused = 
                new GamePauseState(_stateMachine, _game, _settingsOpener);
            var gameEnd = 
                new GameEndState(_stateMachine, _game, _settingsOpener, _progressSavingSystem);
            var gameRestart = 
                new RestartGameState(_stateMachine, _game);

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