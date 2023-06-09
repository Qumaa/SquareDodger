using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class SettingsMenuHandler : GameCanvasUI, ISettingsMenu
    {
        [SerializeField] private Button _closeSettingsButton;

        public event Action OnClosePressed;

        protected override void OnAwake()
        {
            _closeSettingsButton.onClick.AddListener(HandleCloseSettingsPressed);
        }

        private void HandleCloseSettingsPressed()
        {
            Hide();
            OnClosePressed?.Invoke();
        }
    }
}