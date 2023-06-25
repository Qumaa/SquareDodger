using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class GameEndMenuHandler : GameCanvasUI, IGameEndMenu
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _returnToMenuButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private GameEndMenuScoreDisplay _scoreDisplay;
        
        public event Action OnRestartGamePressed;
        public event Action OnReturnToMenuPressed;
        public event Action OnOpenSettingsPressed;

        protected override void OnAwake()
        {
            _restartButton.onClick.AddListener(RestartGame);
            _returnToMenuButton.onClick.AddListener(HandleReturnToMenuPressed);
            _settingsButton.onClick.AddListener(Settings);

        }

        private void RestartGame()
        {
            OnRestartGamePressed?.Invoke();
        }

        private void HandleReturnToMenuPressed()
        {
            OnReturnToMenuPressed?.Invoke();
        }

        private void Settings()
        {
            OnOpenSettingsPressed?.Invoke();
        }

        public void DisplayScore(float score)
        {
            _scoreDisplay.DisplayScore(score);
        }

        public void SetHighestScore(float score)
        {
            _scoreDisplay.SetHighestScore(score);
        }
    }

}
