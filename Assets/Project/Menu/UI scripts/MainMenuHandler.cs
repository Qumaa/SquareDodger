using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class MainMenuHandler : GameInputCanvasUI, IMainMenu
    {
        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _settingsButton;

        public event Action OnGameStartPressed;
        public event Action OnApplicationQuitPressed;
        public event Action OnOpenSettingsPressed;

        protected override void OnAwake()
        {
            _quitButton.onClick.AddListener(QuitGame);
            _settingsButton.onClick.AddListener(Settings);
        }

        private void GameStart()
        {
            OnGameStartPressed?.Invoke();
        }

        private void QuitGame()
        {
            OnApplicationQuitPressed?.Invoke();
        }
        
        private void Settings()
        {
            OnOpenSettingsPressed?.Invoke();
        }

        protected override Action GetInputHandler() =>
            GameStart;
    }
}
