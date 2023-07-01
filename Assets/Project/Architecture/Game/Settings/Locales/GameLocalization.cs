using Project.Game;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Project.Architecture
{
    public static class GameLocalization
    {
        public static void SetLocale(GameLocale locale) =>
            SetLocale(LocaleEnumToLocaleIndex(locale));

        public static LocaleIdentifier GetLocaleIdentifier(GameLocale locale) =>
            LocalizationSettings.AvailableLocales.Locales[LocaleEnumToLocaleIndex(locale)].Identifier;

        public static GameLocale GetCurrentLocale() =>
            (GameLocale) LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);

        private static void SetLocale(int index) =>
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

        private static int LocaleEnumToLocaleIndex(GameLocale enumEntry) =>
            (int) enumEntry;
    }
}