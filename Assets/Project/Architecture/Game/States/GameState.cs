namespace Project.Architecture
{
    public abstract class GameState : IGameState
    {
        protected readonly IGameStateMachine _stateMachine;
        protected readonly IGame _game;

        protected GameState(IGameStateMachine stateMachine, IGame game)
        {
            _stateMachine = stateMachine;
            _game = game;
        }

        public abstract void Enter();

        public abstract void Exit();
    }
}