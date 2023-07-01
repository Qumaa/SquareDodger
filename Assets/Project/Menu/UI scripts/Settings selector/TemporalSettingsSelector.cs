using System;
using System.Collections.Generic;
using Project.Game;
using TMPro;
using UnityEngine.UI;

namespace Project.UI
{
    public class TemporalSettingsSelector : SettingsSelector
    {
        private Slider _masterSlider;
        private Slider _soundsSlider;
        private Slider _musicSlider;

        private TMP_Dropdown _themeSelector;
        private Toggle _darkThemeToggle;

        private TMP_Dropdown _shaderModeSelector;
        private TMP_Dropdown _localeSelector;

        public TemporalSettingsSelector(Slider masterSlider, Slider soundsSlider, Slider musicSlider,
            TMP_Dropdown themeSelector, Toggle darkThemeToggle, TMP_Dropdown shaderModeSelector,
            TMP_Dropdown localeSelector)
        {
            _masterSlider = masterSlider;
            _soundsSlider = soundsSlider;
            _musicSlider = musicSlider;
            _themeSelector = themeSelector;
            _darkThemeToggle = darkThemeToggle;
            _shaderModeSelector = shaderModeSelector;
            _localeSelector = localeSelector;

            InitInputElements();
        }

        protected override void UnsubscribeEventsFromData()
        {
            _masterSlider.onValueChanged?.RemoveListener(SetMasterVolume);
            _soundsSlider.onValueChanged?.RemoveListener(SetSoundsVolume);
            _musicSlider.onValueChanged?.RemoveListener(SetMusicVolume);

            _themeSelector.onValueChanged?.RemoveListener(SetThemeBasedOnIndex);
            _darkThemeToggle.onValueChanged?.RemoveListener(SetThemeMode);

            _shaderModeSelector.onValueChanged?.RemoveListener(SetShaderModeBasedOnIndex);

            _localeSelector.onValueChanged?.RemoveListener(SetLocaleBasedOnIndex);
        }

        protected override void SubscribeEventsToData()
        {
            _masterSlider.onValueChanged.AddListener(SetMasterVolume);
            _soundsSlider.onValueChanged.AddListener(SetSoundsVolume);
            _musicSlider.onValueChanged.AddListener(SetMusicVolume);

            _themeSelector.onValueChanged.AddListener(SetThemeBasedOnIndex);
            _darkThemeToggle.onValueChanged.AddListener(SetThemeMode);

            _shaderModeSelector.onValueChanged.AddListener(SetShaderModeBasedOnIndex);

            _localeSelector.onValueChanged.AddListener(SetLocaleBasedOnIndex);
        }

        protected override void UpdateUIToMatchData(PlayerSettingsData data)
        {
            _masterSlider.value = data.MasterVolume;
            _soundsSlider.value = data.SoundsVolume;
            _musicSlider.value = data.MusicVolume;

            _themeSelector.value = (int) data.CurrentTheme;
            _darkThemeToggle.isOn = data.IsCurrentThemeDark;

            _shaderModeSelector.value = (int) data.ShaderMode;

            _localeSelector.value = (int) data.GameLocale;
        }

        private void InitInputElements()
        {
            FillDropdownWithEnumEntries<GameTheme>(_themeSelector);
            FillDropdownWithEnumEntries<ShaderBlendingMode>(_shaderModeSelector);
            FillDropdownWithEnumEntries<GameLocale>(_localeSelector);
        }

        private void SetThemeBasedOnIndex(int optionIndex) =>
            SetThemeType((GameTheme) optionIndex);

        private void SetShaderModeBasedOnIndex(int optionIndex) =>
            SetShaderMode((ShaderBlendingMode) optionIndex);

        private void SetLocaleBasedOnIndex(int optionIndex) =>
            SetLocale((GameLocale) optionIndex);

        private static void FillDropdownWithEnumEntries<T>(TMP_Dropdown dropdown)
            where T : Enum
        {
            var enumType = typeof(T);
            var items = Enum.GetNames(enumType).Length;
            var newOptions = new List<TMP_Dropdown.OptionData>(items);

            for (var i = 0; i < items; i++)
                newOptions.Add(new TMP_Dropdown.OptionData(Enum.GetName(enumType, i)));

            dropdown.ClearOptions();
            dropdown.AddOptions(newOptions);
        }
    }
}