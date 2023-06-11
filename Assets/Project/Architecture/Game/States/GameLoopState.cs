namespace Project.Architecture
{
    public class GameLoopState : GameState
    {

        public GameLoopState(IGameStateMachine stateMachine, IGame game) : base(stateMachine, game)
        {
        }

        public override void Enter()
        {
            _game.Gameplay.Resume();
            _game.Gameplay.OnEnded += HandleGameEnd;
        }

        private void HandleGameEnd()
        {
            _stateMachine.SetState<GameEndState>();
        }

        private void HandleGamePause()
        {
            _stateMachine.SetState<GamePauseState>();
        }

        public override void Exit()
        {
        }
    }
}