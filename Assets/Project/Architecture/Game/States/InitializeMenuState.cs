namespace Project.Architecture
{
    public class InitializeMenuState : GameState
    {
        private IGameLoader _gameLoader;
        private IGame _game;

        public InitializeMenuState(IGameStateMachine stateMachine, IGameLoader gameLoader, Game game) : base(stateMachine)
        {
            _gameLoader = gameLoader;
            _game = game;
        }

        public override void Enter()
        {
            _gameLoader.Load(_game);
            _stateMachine.SetState<MenuState>();
        }

        public override void Exit()
        {
        }
    }
}