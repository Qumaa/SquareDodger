namespace Project.Architecture
{
    public class InitializeMenuState : GameState
    {
        public InitializeMenuState(IGameStateMachine stateMachine, IGame game) : base(stateMachine, game)
        {
        }

        public override void Enter()
        {
            _game.MainMenu.SetCamera(_game.CameraController.ControlledCamera);
            _stateMachine.SetState<MenuState>();
        }

        public override void Exit()
        {
        }
    }
}