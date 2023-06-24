using Project.Game;
using Project.UI;

namespace Project.Architecture
{
    public class GameLoopState : GameState
    {
        private IGameplayUI _gameplayUI;
        private DisplayUpdater _displayUpdater;
        private ISavingSystem<PlayerProgressData> _progressSavingSystem;
        private PlayerProgressData _playerProgress;

        public GameLoopState(IGameStateMachine stateMachine, IGame game, ISavingSystem<PlayerProgressData> progressSavingSystem) : 
            base(stateMachine, game)
        {
            _progressSavingSystem = progressSavingSystem;
            _playerProgress = _progressSavingSystem.LoadData();
        }

        public override void Enter()
        {
            GetMenuIfNecessary();
            
            _game.Gameplay.Resume();
            _game.Gameplay.OnEnded += HandleGameEnd;
            
            _gameplayUI.Show();
            _gameplayUI.OnPausePressed += HandleGamePause;
            
            _game.Add(_displayUpdater);
            _displayUpdater.SetHighestScore(_playerProgress.HighestScore);
            _displayUpdater.UpdateDisplay();
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
            UpdateHighestScore();
            _stateMachine.SetState<GameEndState>();
        }

        private void UpdateHighestScore()
        {
            var highest = _playerProgress.HighestScore;
            var current = _game.Gameplay.Score;

            if (current <= highest)
                return;

            _playerProgress.HighestScore = current;
            _progressSavingSystem.SaveData(_playerProgress);
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

            public void FixedUpdate(float fixedTimeStep) =>
                UpdateDisplay();

            public void UpdateDisplay() =>
                _display.DisplayScore(_scoreSource.Score);

            public void SetHighestScore(float score) =>
                _display.SetHighestScore(score);
        }
    }
}