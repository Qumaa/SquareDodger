using System;
using Project.UI;

namespace Project.Architecture
{
    public class GameEndState : GameState
    {
        private IGameEndMenu _endMenu;
        private ISettingsMenuOpener _settingsOpener;
        
        public GameEndState(IGameStateMachine stateMachine, IGame game, ISettingsMenuOpener settingsOpener) : base(stateMachine, game)
        {
            _settingsOpener = settingsOpener;
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
            _endMenu.OnRestartGamePressed -= HandleGameRestart;
            _endMenu.OnReturnToMenuPressed -= HandleReturnToMenu;
            _endMenu.OnOpenSettingsPressed -= HandleSettings;
        }

        private void HandleGameRestart()
        {
            _stateMachine.SetState<RestartGameState>();
        }

        private void HandleReturnToMenu()
        {
            _game.Gameplay.Reset();
            _stateMachine.SetState<MenuState>();
        }

        private void HandleSettings()
        {
            _settingsOpener.OpenSettings();
        }

        private void GetMenuIfNecessary() =>
            _endMenu ??= _game.UI.Get<IGameEndMenu>();
    }
}