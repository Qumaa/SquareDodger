using UnityEngine;
using System.IO;

namespace Project.Game
{
    public class PlayerSettingsSavingSystem : BinaryFormatterSavingSystem<PlayerSettingsData>
    {
        private const string _FILE_NAME = "prefs.data";

        public PlayerSettingsSavingSystem() : 
            base(Path.Combine(Application.persistentDataPath, _FILE_NAME))
        {
        }

        protected override PlayerSettingsData CreateEmptyDataInstance() => 
            new();
    }
}