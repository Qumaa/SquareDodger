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
    }

}
