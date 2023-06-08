namespace Project.Architecture
{
    public class MenuState : GameState
    {

        public MenuState(IGameStateMachine stateMachine, IGame game) : base(stateMachine, game)
        {
        }

        public override void Enter()
        {
            _game.MainMenu.Show();
            _game.MainMenu.OnGameStartPressed += HandleGameStart;
        }

        public override void Exit()
        {
            _game.MainMenu.Hide();
        }

        private void HandleGameStart()
        {
            _stateMachine.SetState<GameLoopState>();
        }
    }
}