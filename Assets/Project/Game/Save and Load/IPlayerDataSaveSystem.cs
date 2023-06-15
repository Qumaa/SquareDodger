using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Project
{
    public interface IPlayerDataSaveSystem
    {
        void SaveData(PlayerData data);
        PlayerData LoadData();
    }
    
}
