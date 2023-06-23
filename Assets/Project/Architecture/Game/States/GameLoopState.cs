using Project.Game;
using Project.UI;

namespace Project.Architecture
{
    public class GameLoopState : GameState
    {
        private IGameplayUI _gameplayUI;
        private DisplayUpdater _displayUpdater;

        public GameLoopState(IGameStateMachine stateMachine, IGame game) : base(stateMachine, game)
        {
        }

        public override void Enter()
        {
            GetMenuIfNecessary();
            
            _game.Gameplay.Resume();
            _game.Gameplay.OnEnded += HandleGameEnd;
            
            _gameplayUI.Show();
            _gameplayUI.OnPausePressed += HandleGamePause;
            
            _game.Add(_displayUpdater);
            // this is absolutely fucking stupid
            // but there is no other way to repaint display
            _displayUpdater.FixedUpdate(0);
        }

        public override void Exit()
        {
            _game.Gameplay.OnEnded -= HandleGameEnd;
            
            _gameplayUI.Hide();
            _gameplayUI.OnPausePressed -= HandleGamePause;
            
            _game.Remove(_displayUpdater);
        }

        private void HandleGameEnd()
        {
            _stateMachine.SetState<GameEndState>();
        }

        private void HandleGamePause()
        {
            _stateMachine.SetState<GamePauseState>();
        }

        private void GetMenuIfNecessary()
        {
            if (_gameplayUI != null)
                return;
            
            _gameplayUI = _game.UI.Get<IGameplayUI>();
            _displayUpdater = new DisplayUpdater(_game.Gameplay, _gameplayUI);
        }

        private class DisplayUpdater : IFixedUpdatable
        {
            private IScoreSource _scoreSource;
            private IGameScoreDisplay _display;

            public DisplayUpdater(IScoreSource scoreSource, IGameScoreDisplay display)
            {
                _scoreSource = scoreSource;
                _display = display;
            }

            public void FixedUpdate(float fixedTimeStep)
            {
                _display.DisplayScore(_scoreSource.Score);
            }
        }
    }
}