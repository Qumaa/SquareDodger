using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class SettingsMenuHandler : CanvasGameUI, ISettingsMenu
    {
        [SerializeField] private Button _closeSettingsButton;

        public event Action OnClosePressed;

        protected override void OnAwake()
        {
            _closeSettingsButton.onClick.AddListener(HandleCloseSettingsPressed);
        }

        private void HandleCloseSettingsPressed()
        {
            OnClosePressed?.Invoke();
        }
    }
}