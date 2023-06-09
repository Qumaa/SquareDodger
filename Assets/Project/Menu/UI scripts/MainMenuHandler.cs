using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class MainMenuHandler : GameCanvasUI, IMainMenu
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _settingsButton;

        public event Action OnGameStartPressed;
        public event Action OnApplicationQuitPressed;
        public event Action OnOpenSettingsPressed;

        protected override void OnAwake()
        {
            _startButton.onClick.AddListener(GameStart);
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
            InvokeTapped();
        }
        
        private void Settings()
        {
            OnOpenSettingsPressed?.Invoke();
            InvokeTapped();
        }
    }
}
