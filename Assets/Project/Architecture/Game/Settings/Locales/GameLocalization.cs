using System;
using Project.Game;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

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
            if (!LocalizationSettings.InitializationOperation.IsDone)
                return;
            
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        }

        private static int LocaleEnumToLocaleIndex(GameLocale enumEntry) =>
            (int) enumEntry;
    }
}