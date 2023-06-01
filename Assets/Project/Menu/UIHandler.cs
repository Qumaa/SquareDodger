using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class UIHandler : MonoBehaviour, IGameMenu
    {
        private Canvas _canvas;

        private void Start()
        {
            GetComponentInChildren<Button>().onClick.AddListener(GameStart);
            _canvas = GetComponent<Canvas>();
        }

        private void GameStart()
        {
            _canvas.gameObject.SetActive(false);
            OnGameStart.Invoke();
        }

        public event Action OnGameStart;
        public void SetCamera(Camera uiCamera)
        {
            _canvas.worldCamera = uiCamera;
        }
    }

    public interface IGameMenu
    {
        event Action OnGameStart;
        void SetCamera(Camera uiCamera);
    }
}
