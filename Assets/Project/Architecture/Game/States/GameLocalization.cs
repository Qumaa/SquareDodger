using Project.Game;
using UnityEngine.Localization.Settings;

namespace Project.Architecture
{
    public static class GameLocalization
    {
        public static void SetLocale(GameLocale locale) =>
            SetLocale(LocaleEnumToLocaleIndex(locale));
        
        private static void SetLocale(int index) =>
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        
        private static int LocaleEnumToLocaleIndex(GameLocale enumEntry) =>
            (int) enumEntry;
    }
}