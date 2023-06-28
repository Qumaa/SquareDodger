using System;
using Project.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class SettingsMenuHandler : GameCanvasUI, ISettingsMenu
    {
        [SerializeField] private Button _closeSettingsButton;
        [SerializeField] private TMP_Dropdown _themeSelector;
        [SerializeField] private Toggle _darkThemeToggle;
        
        private ISettingsSelector _settingsSelector;
        
        public event Action OnClosePressed;

        protected override void OnAwake()
        {
            _closeSettingsButton.onClick.AddListener(HandleCloseSettingsPressed);

            _settingsSelector = new TemporalSettingsSelector(_themeSelector, _darkThemeToggle);
        }

        public void SetSettingsData(PlayerSettingsData data)
        {
            _settingsSelector.SetData(data);
        }

        private void HandleCloseSettingsPressed()
        {
            OnClosePressed?.Invoke();
            InvokeTapped();
        }
    }
}