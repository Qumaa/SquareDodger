using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class MainMenuHandler : MonoBehaviour, IMainMenu
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button returnButton;
        private Canvas _canvas;

        public event Action OnGameStartPressed;
        public event Action OnApplicationQuitPressed;
        public event Action OnOpenSettingsPressed;
        public event Action OnBackMenuPressed;

        private void Awake()
        {
           
            playButton.onClick.AddListener(GameStart);
            quitButton.onClick.AddListener(QuitGame);
            settingsButton.onClick.AddListener(Settings);
            returnButton.onClick.AddListener(ReturnToMenu);
            
            _canvas = GetComponent<Canvas>();
        }

        private void GameStart()
        {
            OnGameStartPressed.Invoke();
        }

        private void QuitGame()
        {
            OnApplicationQuitPressed?.Invoke();
        }

        private void ReturnToMenu()
        {
            OnBackMenuPressed?.Invoke();
            
        }

        private void Settings()
        {
            OnOpenSettingsPressed?.Invoke();
        }

        public void SetCamera(Camera uiCamera)
        {
            _canvas.worldCamera = uiCamera;
            _canvas.planeDistance = 1;
        }

        public void Show()
        {
            _canvas.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _canvas.gameObject.SetActive(false);
        }
    }
}
