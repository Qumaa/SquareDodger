using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class MainMenuHandler : MonoBehaviour, IMainMenu
    {
        private Canvas _canvas;

        public event Action OnGameStartPressed;
        public event Action OnApplicationQuitPressed;
        public event Action OnOpenSettingsPressed;

        private void Awake()
        {
            GetComponentInChildren<Button>().onClick.AddListener(GameStart);
            _canvas = GetComponent<Canvas>();
        }

        private void GameStart()
        {
            OnGameStartPressed.Invoke();
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
