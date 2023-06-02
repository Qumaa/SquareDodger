using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public class Game : IGame
    {
        private IGameLoader _gameLoader;
        private IGameStateMachine _stateMachine;

        private Camera _camera;

        public IGameplay Gameplay { get; set; }
        public IMainMenu MainMenu { get; set; }

        public Game(IGameLoader gameLoader, Camera camera)
        {
            _gameLoader = gameLoader;
            _camera = camera;
            InitializeStateMachine();
        }

        public void Initialize()
        {
            MainMenu.SetCamera(_camera);
            MainMenu.OnGameStartPressed += HandleGameStart;
        }

        private void HandleGameStart()
        {
            MainMenu.Hide();
            Gameplay.Resume();
        }

        public void Run()
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
            var bootstrap = new BootstrapState(_stateMachine);
            var initializeMenu = new InitializeMenuState(_stateMachine, _gameLoader, this);
            var menuState = new MenuState(_stateMachine);
            var gameLoop = new GameLoopState(_stateMachine);

            _stateMachine.AddState(bootstrap)
                .AddState(initializeMenu)
                .AddState(menuState)
                .AddState(gameLoop);
        }

        public void Update(float timeStep)
        {
            Gameplay.Update(timeStep);
        }

        public void FixedUpdate(float fixedTimeStep)
        {
            Gameplay.FixedUpdate(fixedTimeStep);
        }
    }
}