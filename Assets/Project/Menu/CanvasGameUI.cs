using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    public abstract class CanvasGameUI : MonoBehaviour, IGameUI
    {
        protected Canvas _canvas { get; private set; }

        public virtual void Hide()
        {
            _canvas.gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            _canvas.gameObject.SetActive(true);
        }

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            OnAwake();
        }

        protected abstract void OnAwake();

        public void SetCamera(Camera uiCamera)
        {
            if (_canvas == null)
                _canvas = GetComponent<Canvas>();

            _canvas.worldCamera = uiCamera;
            _canvas.planeDistance = 1;
        }
    }
}