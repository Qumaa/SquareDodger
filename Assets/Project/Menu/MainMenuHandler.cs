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
        [SerializeField] private Button backmenuButton;
        [SerializeField] private Button backgameButton;
        
        private Canvas _canvas;
       
        

        public event Action OnGameStartPressed;
        public event Action OnApplicationQuitPressed;
        public event Action OnOpenSettingsPressed;
        public event Action OnBackMenuPressed;
        public event Action OnMenuPressed;
        public event Action OnGameReturnPressed;
        
        

        private void Awake()
        {
           
            playButton.onClick.AddListener(GameStart);
            quitButton.onClick.AddListener(QuitGame);
            settingsButton.onClick.AddListener(Settings);
            returnButton.onClick.AddListener(ReturnToMenu);
            backmenuButton.onClick.AddListener(BackToMenu);
            backgameButton.onClick.AddListener(BackToGame);
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

        private void BackToMenu()
        {
            OnMenuPressed?.Invoke();
        }
        
        private void BackToGame()
        {
            OnGameReturnPressed?.Invoke();
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
