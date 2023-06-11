using Project.Game;
using Project.UI;

namespace Project.Architecture
{
    public class GameLoopState : GameState
    {
        private IGameplayUI _gameplayUI;
        private UIUpdater _updater;

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
            
            _game.Add(_updater);
        }

        public override void Exit()
        {
            _game.Gameplay.OnEnded -= HandleGameEnd;
            
            _gameplayUI.Hide();
            _gameplayUI.OnPausePressed -= HandleGamePause;
            
            _game.Remove(_updater);
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
            _updater = new UIUpdater(_game.Gameplay, _gameplayUI);
        }

        private class UIUpdater : IFixedUpdatable
        {
            private IScoreSource _scoreSource;
            private IGameScoreDisplayer _displayer;

            public UIUpdater(IScoreSource scoreSource, IGameScoreDisplayer displayer)
            {
                _scoreSource = scoreSource;
                _displayer = displayer;
            }

            public void FixedUpdate(float fixedTimeStep)
            {
                _displayer.DisplayScore(_scoreSource.Score);
            }
        }
    }
}