using System;
using System.Collections.Generic;
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

        private PlayerSettingsData _settingsData;
        
        public event Action OnClosePressed;

        protected override void OnAwake()
        {
            _closeSettingsButton.onClick.AddListener(HandleCloseSettingsPressed);

            InitInputElements();
        }

        public void SetSettingsData(PlayerSettingsData data)
        {
            UnsubscribeEventsFromCurrentData();
            _settingsData = data;
            UpdateInputElementsToMatchData();
            SubscribeEventsToCurrentData();
        }

        private void InitInputElements()
        {
            // theme dropdown
            var enumType = typeof(GameThemes);
            var items = Enum.GetNames(enumType).Length;
            var newOptions = new List<string>(items);

            for(var i = 0; i < items; i++)
                newOptions.Add(Enum.GetName(enumType, i));

            _themeSelector.ClearOptions();
            _themeSelector.AddOptions(newOptions);
        }

        private void UnsubscribeEventsFromCurrentData()
        {
            _themeSelector.onValueChanged?.RemoveListener(SetThemeType);
            _darkThemeToggle.onValueChanged?.RemoveListener(SetThemeMode);
        }

        private void UpdateInputElementsToMatchData()
        {
            _themeSelector.value = (int)_settingsData.CurrentTheme;
            _darkThemeToggle.isOn = _settingsData.IsCurrentThemeDark;
        }

        private void SubscribeEventsToCurrentData()
        {
            _themeSelector.onValueChanged.AddListener(SetThemeType);
            _darkThemeToggle.onValueChanged.AddListener(SetThemeMode);
        }

        private void SetThemeType(int option)
        {
            _settingsData.CurrentTheme = (GameThemes)option;
        }

        private void SetThemeMode(bool dark)
        {
            _settingsData.IsCurrentThemeDark = dark;
        }

        private void HandleCloseSettingsPressed()
        {
            OnClosePressed?.Invoke();
            InvokeTapped();
        }
    }
}