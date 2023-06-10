using System;
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

        public event Action OnEnded;
        
        public IGameplay Gameplay { get; set; }
        public IGameCanvasUIRenderer UI { get; set; }
        public ICameraController CameraController { get; set; }
        public IGameInputService InputService { get; set; }

        public Game(GameRuntimeData gameData, Camera camera, IDisposer disposer)
        {
            _gameData = gameData;
            
            _camera = camera;
            _disposer = disposer;
            InitializeStateMachine();
        }

        public void Update(float timeStep)
        {
            Gameplay.Update(timeStep);
        }

        public void FixedUpdate(float fixedTimeStep)
        {
            Gameplay.FixedUpdate(fixedTimeStep);
        }

        public void Initialize()
        {
            _stateMachine.SetState<BootstrapState>();
        }

        private void InitializeStateMachine()
        {
            _stateMachine = new GameStateMachine();
            InitializeStates();
        }

        private void InitializeStates()
        {
            var bootstrap = new BootstrapState(_stateMachine, this, _disposer, _gameData, _camera);
            var initializeMenu = new InitializeUIState(_stateMachine, this, _gameData.GameUIData);
            var menuState = new MenuState(_stateMachine, this);
            var gameLoop = new GameLoopState(_stateMachine, this);
            var gamePaused = new GamePauseState(_stateMachine, this);
            var gameEnd = new GameEndState(_stateMachine, this);
            var gameRestart = new RestartGameState(_stateMachine, this);

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