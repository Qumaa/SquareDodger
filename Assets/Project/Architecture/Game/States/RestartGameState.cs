using System;

namespace Project.Architecture
{
    public class RestartGameState : GameState
    {
        public RestartGameState(IGameStateMachine stateMachine) : base(stateMachine)
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