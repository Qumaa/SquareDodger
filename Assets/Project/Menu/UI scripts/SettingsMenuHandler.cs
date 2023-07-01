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

        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _soundsSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private TMP_Dropdown _themeSelector;
        [SerializeField] private Toggle _darkThemeToggle;
        [SerializeField] private TMP_Dropdown _shaderModeSelector;
        [SerializeField] private TMP_Dropdown _localeSelector;

        private ISettingsSelector _settingsSelector;

        public event Action OnClosePressed;

        protected override void OnAwake()
        {
            _closeSettingsButton.onClick.AddListener(HandleCloseSettingsPressed);

            _settingsSelector = new TemporalSettingsSelector(_masterSlider, _soundsSlider, _musicSlider, _themeSelector,
                _darkThemeToggle, _shaderModeSelector, _localeSelector);
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