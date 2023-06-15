using UnityEngine;
using System.IO;

namespace Project.Game
{
    public class PlayerSettingsSaveSystem : BinaryFormatterSavingSystem<PlayerSettingsData>
    {
        private const string _FILE_NAME = "prefs.data";

        public PlayerSettingsSaveSystem() : 
            base(Path.Combine(Application.persistentDataPath, _FILE_NAME))
        {
        }

        protected override PlayerSettingsData CreateNewGenericInstance() => 
            new();
    }
}