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

        public IGameplay Gameplay { get; set; }
        public IGameCanvasUIRenderer GameCanvasUI { get; set; }
        public ICameraController CameraController { get; set; }

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
            // TODO: move this the fuck outta here
            var mainMenu = GameObject.Instantiate(_gameData.GameUIData.MainMenuPrefab).GetComponent<IMainMenu>();
            
            var bootstrap = new BootstrapState(_stateMachine, this, _disposer, _gameData, _camera);
            var initializeMenu = new InitializeMenuState(_stateMachine, this, mainMenu);
            var menuState = new MenuState(_stateMachine, this, mainMenu);
            var gameLoop = new GameLoopState(_stateMachine, this);

            _stateMachine.AddState(bootstrap)
                .AddState(initializeMenu)
                .AddState(menuState)
                .AddState(gameLoop);
        }
    }
}