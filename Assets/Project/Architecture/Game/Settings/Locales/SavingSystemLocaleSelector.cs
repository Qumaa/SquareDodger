using Project.Game;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Project.Architecture
{
    public class SavingSystemLocaleSelector : IStartupLocaleSelector
    {
        private PlayerSettingsSavingSystem _savingSystem;

        public SavingSystemLocaleSelector(PlayerSettingsSavingSystem savingSystem)
        {
            _savingSystem = savingSystem;
            DestroyOnLoadComplete();
        }

        public Locale GetStartupLocale(ILocalesProvider availableLocales)
        {
            if (!_savingSystem.HasSavedData)
                return null;
            
            var data = _savingSystem.LoadData();

            var startupLocale = availableLocales.GetLocale(GameLocalization.GetLocaleIdentifier(data.GameLocale));
            
            return startupLocale;
        }

        private void DestroyOnLoadComplete()
        {
            LocalizationSettings.InitializationOperation.Completed +=
                _ => LocalizationSettings.StartupLocaleSelectors.Remove(this);
        }
    }
}