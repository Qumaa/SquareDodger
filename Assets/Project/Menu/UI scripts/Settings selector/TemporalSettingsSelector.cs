using System;
using System.Collections.Generic;
using Project.Game;
using TMPro;
using UnityEngine.UI;

namespace Project.UI
{
    public class TemporalSettingsSelector : SettingsSelector
    {
        private TMP_Dropdown _themeSelector;
        private Toggle _darkThemeToggle;

        public TemporalSettingsSelector(TMP_Dropdown themeSelector, Toggle darkThemeToggle)
        {
            _themeSelector = themeSelector;
            _darkThemeToggle = darkThemeToggle;
            
            InitInputElements();
        }

        protected override void UnsubscribeEventsFromData(PlayerSettingsData data)
        {
            _themeSelector.onValueChanged?.RemoveListener(SetThemeBasedOnIndex);
            _darkThemeToggle.onValueChanged?.RemoveListener(SetThemeMode);
        }

        protected override void SubscribeEventsToData(PlayerSettingsData data)
        {
            _themeSelector.onValueChanged.AddListener(SetThemeBasedOnIndex);
            _darkThemeToggle.onValueChanged.AddListener(SetThemeMode);
        }

        protected override void UpdateUIToMatchData(PlayerSettingsData data)
        {
            _themeSelector.value = (int)data.CurrentTheme;
            _darkThemeToggle.isOn = data.IsCurrentThemeDark;
        }
        
        private void InitInputElements()
        {
            // theme dropdown
            var enumType = typeof(GameTheme);
            var items = Enum.GetNames(enumType).Length;
            var newOptions = new List<string>(items);

            for(var i = 0; i < items; i++)
                newOptions.Add(Enum.GetName(enumType, i));

            _themeSelector.ClearOptions();
            _themeSelector.AddOptions(newOptions);
        }
        
        private void SetThemeBasedOnIndex(int optionIndex) =>
            SetThemeType((GameTheme) optionIndex);
    }
}