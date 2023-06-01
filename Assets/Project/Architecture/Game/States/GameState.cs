namespace Project.Architecture
{
    public abstract class GameState : IGameState
    {
        protected readonly IGameStateMachine _stateMachine;

        protected GameState(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public abstract void Enter();

        public abstract void Exit();
    }
}