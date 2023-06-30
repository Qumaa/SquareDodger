using System;
using Project.Game;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Project.Architecture
{
    public static class GameLocalization
    {
        public static void SetLocale(GameLocale locale) =>
            SetLocale(LocaleEnumToLocaleIndex(locale));

        public static void AddGameLocaleSelector(IStartupLocaleSelector selector) =>
            LocalizationSettings.StartupLocaleSelectors.Insert(1, selector);

        public static LocaleIdentifier GetLocaleIdentifier(GameLocale locale) =>
            LocalizationSettings.AvailableLocales.Locales[LocaleEnumToLocaleIndex(locale)].Identifier;

        private static void SetLocale(int index)
        {
            var loadingOperation = LocalizationSettings.InitializationOperation;
            
            if (loadingOperation.IsDone)
            {
                SetLocaleInternal(index);
                return;
            }

            loadingOperation.Completed += _ => SetLocaleInternal(index);
        }

        private static void SetLocaleInternal(int index) =>
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

        private static int LocaleEnumToLocaleIndex(GameLocale enumEntry) =>
            (int) enumEntry;
    }
}