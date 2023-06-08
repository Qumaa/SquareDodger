using System;

namespace Project.Architecture
{
    public class GamePauseState : GameState
    {
        public GamePauseState(IGameStateMachine stateMachine, IGame game) : base(stateMachine, game)
        {
        }

        public override void Enter()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }
    }
}