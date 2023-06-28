using Project.Game;
using UnityEditor;

#if UNITY_EDITOR

namespace Project
{
    public static class MenuItems
    {
        private const string _ROOT = "Project/";
        
        private const string _SAVED_DATA = _ROOT + "Saved data/";
        private const string _ALL = _SAVED_DATA + "Clear all";
        private const string _PREFS = _SAVED_DATA + "Clear preferences";
        private const string _PROGRESS = _SAVED_DATA + "Clear progress";

        [MenuItem(_ALL)]
        public static void ClearAll()
        {
            ClearProgress();
            ClearPrefs();
        }

        [MenuItem(_PROGRESS)]
        public static void ClearProgress()
        {
            var saveSystem = new PlayerProgressSavingSystem();
            var emptyData = new PlayerProgressData();
            saveSystem.SaveData(emptyData);
        }
        
        [MenuItem(_PREFS)]
        private static void ClearPrefs()
        {
            var saveSystem = new PlayerSettingsSavingSystem();
            var emptyData = new PlayerSettingsData();
            saveSystem.SaveData(emptyData);
        }
    }
}

#endif
