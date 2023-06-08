using System;

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
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }
    }
}