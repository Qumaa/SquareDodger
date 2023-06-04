using Project.Game;
using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public class Game : IGame
    {
        private IGameStateMachine _stateMachine;

        private GameConfig _gameConfig;
        private Camera _camera;
        private IDisposer _disposer;
        private GameObject _uiPrefab;

        public IGameplay Gameplay { get; set; }
        public IMainMenu MainMenu { get; set; }
        public ICameraController CameraController { get; set; }

        public Game(GameConfig gameConfig, Camera camera, IDisposer disposer, GameObject uiPrefab)
        {
            _gameConfig = gameConfig;
            _camera = camera;
            _disposer = disposer;
            _uiPrefab = uiPrefab;
            InitializeStateMachine();
        }

        public void Run()
        {
            MainMenu.SetCamera(_camera);
            MainMenu.OnGameStartPressed += HandleGameStart;
        }

        public void Update(float timeStep)
        {
            Gameplay.Update(timeStep);
        }

        public void FixedUpdate(float fixedTimeStep)
        {
            Gameplay.FixedUpdate(fixedTimeStep);
        }

        private void HandleGameStart()
        {
            MainMenu.Hide();
            Gameplay.Resume();
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
            var bootstrap = new BootstrapState(_stateMachine, this, _disposer, _gameConfig, _uiPrefab, _camera);
            var initializeMenu = new InitializeMenuState(_stateMachine, this);
            var menuState = new MenuState(_stateMachine);
            var gameLoop = new GameLoopState(_stateMachine);

            _stateMachine.AddState(bootstrap)
                .AddState(initializeMenu)
                .AddState(menuState)
                .AddState(gameLoop);
        }
    }
}