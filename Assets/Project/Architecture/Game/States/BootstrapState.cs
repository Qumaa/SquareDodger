using UnityEngine;

namespace Project.Architecture
{
    public class BootstrapState : GameState
    {

        public BootstrapState(IGameStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            InitializeServices();
            MoveToMenu();
        }

        private void InitializeServices()
        {
            // sound, input etc.
        }

        private void MoveToMenu()
        {
            _stateMachine.SetState<InitializeMenuState>();
        }

        public override void Exit()
        {
            
        }
    }
}