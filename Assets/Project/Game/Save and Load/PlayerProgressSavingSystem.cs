using System.IO;
using UnityEngine;

namespace Project.Game
{
    public class PlayerProgressSavingSystem : BinaryFormatterSavingSystem<PlayerProgressData>
    {
        private const string _FILE_NAME = "progress.data";
        
        public PlayerProgressSavingSystem() : 
            base(Path.Combine(Application.persistentDataPath, _FILE_NAME))
        {
        }

        protected override PlayerProgressData CreateEmptyDataInstance() => 
            new();
    }
}