using System;
using Project.UI;

namespace Project.Architecture
{
    public class GameEndState : GameState
    {
        private IGameEndMenu _endMenu;
        public GameEndState(IGameStateMachine stateMachine, IGame game) : base(stateMachine, game)
        {
        }

        public override void Enter()
        {
            GetMenuIfNecessary();
            
            _endMenu.Show();
            _endMenu.OnRestartGamePressed += HandleGameRestart;
            _endMenu.OnReturnToMenuPressed += HandleReturnToMenu;
            _endMenu.OnOpenSettingsPressed += HandleSettings;
        }

        public override void Exit()
        {
            _endMenu.Hide();
        }

        private void HandleGameRestart()
        {
            _stateMachine.SetState<RestartGameState>();
        }

        private void HandleReturnToMenu()
        {
            _stateMachine.SetState<MenuState>();
        }

        private void HandleSettings()
        {
            throw new NotImplementedException();
        }

        private void GetMenuIfNecessary() =>
            _endMenu ??= _game.UI.Get<IGameEndMenu>();
    }
}