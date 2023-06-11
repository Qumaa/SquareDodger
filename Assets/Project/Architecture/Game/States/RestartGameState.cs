namespace Project.Architecture
{
    public class RestartGameState : GameState
    {
        public RestartGameState(IGameStateMachine stateMachine, IGame game) : base(stateMachine, game)
        {
        }

        public override void Enter()
        {
            _game.Gameplay.Reset();
            _stateMachine.SetState<GameLoopState>();
        }

        public override void Exit()
        {
        }
    }
}