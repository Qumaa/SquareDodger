namespace Project.Architecture
{
    public class InitializeMenuState : GameState
    {
        private IGame _game;

        public InitializeMenuState(IGameStateMachine stateMachine, IGame game) : base(stateMachine)
        {
            _game = game;
        }

        public override void Enter()
        {
            _game.Run();
            _stateMachine.SetState<MenuState>();
        }

        public override void Exit()
        {
        }
    }
}