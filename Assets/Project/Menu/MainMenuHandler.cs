using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class MainMenuHandler : CanvasGameUI, IMainMenu
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _settingsButton;

        public event Action OnGameStartPressed;
        public event Action OnApplicationQuitPressed;
        public event Action OnOpenSettingsPressed;

        protected override void OnAwake()
        {
            _playButton.onClick.AddListener(GameStart);
            _quitButton.onClick.AddListener(QuitGame);
            _settingsButton.onClick.AddListener(Settings);
        }

        private void GameStart()
        {
            Debug.Log("Игра начинается!");
            OnGameStartPressed?.Invoke();
        }

        private void QuitGame()
        {
            Debug.Log("Выход из игры");
            OnApplicationQuitPressed?.Invoke();
        }
        
        private void Settings()
        {
            Debug.Log("Настройки");
            OnOpenSettingsPressed?.Invoke();
        }
    }
}
