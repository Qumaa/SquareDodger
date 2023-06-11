using System;
using Project.UI;

namespace Project.Architecture
{
    public class GamePauseState : GameState
    {
        private IPauseMenu _pauseMenu;
        public GamePauseState(IGameStateMachine stateMachine, IGame game) : base(stateMachine, game)
        {
        }

        public override void Enter()
        {
            GetMenuIfNecessary();
            
            _game.Gameplay.Pause();
            _pauseMenu.Show();

            _pauseMenu.OnContinuePressed += HandleContinue;
            _pauseMenu.OnReturnToMenuPressed += HandleReturnToMenu;
            _pauseMenu.OnOpenSettingsPressed += HandleSettings;
        }

        public override void Exit()
        {
            _pauseMenu.Hide();
            
            _pauseMenu.OnContinuePressed -= HandleContinue;
            _pauseMenu.OnReturnToMenuPressed -= HandleReturnToMenu;
            _pauseMenu.OnOpenSettingsPressed -= HandleSettings;
        }

        private void HandleContinue()
        {
            _game.Gameplay.Resume();
            _stateMachine.SetState<GameLoopState>();
        }

        private void HandleReturnToMenu()
        {
            _game.Gameplay.Reset();
            _stateMachine.SetState<MenuState>();
        }

        private void HandleSettings()
        {
            throw new NotImplementedException();
        }

        private void GetMenuIfNecessary() =>
            _pauseMenu ??= _game.UI.Get<IPauseMenu>();
    }
}