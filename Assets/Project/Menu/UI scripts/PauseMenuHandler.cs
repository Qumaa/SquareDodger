using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class PauseMenuHandler : GameCanvasUI, IPauseMenu
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _returnToMenuButton;
        [SerializeField] private Button _settingsButton;

        public event Action OnContinuePressed;
        public event Action OnReturnToMenuPressed;
        public event Action OnOpenSettingsPressed;

        protected override void OnAwake()
        {
            _resumeButton.onClick.AddListener(HandleContinueGamePressed);
            _returnToMenuButton.onClick.AddListener(HandleReturnToMenuPressed);
            _settingsButton.onClick.AddListener(Settings);
        }

        private void HandleContinueGamePressed()
        {
            OnContinuePressed?.Invoke();
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