using UnityEngine;

namespace Project.Architecture
{
    public class Bootstrapper : MonoBehaviour
    {
        private Camera _gameCamera;
        private IGameStateMachine _stateMachine;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            InitializeCamera();
            InitializeGameStateMachine();
        }

        private void InitializeCamera()
        {
            _gameCamera = Camera.main;
        }

        private void InitializeGameStateMachine()
        {
            _stateMachine = new GameStateMachine();

            InitializeStates();
            
            _stateMachine.SetState<BootstrapState>();
        }

        private void InitializeStates()
        {
            var bootstrap = new BootstrapState(_stateMachine);
            var initializeMenu = new InitializeMenuState(_stateMachine);
            var gameLoop = new GameLoopState(_stateMachine);

            _stateMachine.AddState(bootstrap)
                .AddState(initializeMenu)
                .AddState(gameLoop);
        }
    }
}