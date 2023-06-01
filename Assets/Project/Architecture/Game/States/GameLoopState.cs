using System;

namespace Project.Architecture
{
    public class GameLoopState : GameState
    {
        public GameLoopState(IGameStateMachine stateMachine) : base(stateMachine)
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