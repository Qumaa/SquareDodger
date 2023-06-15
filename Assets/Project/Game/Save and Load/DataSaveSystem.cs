using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Project
{


    public class DataSaveSystem : IPlayerDataSaveSystem
    {
        private string savePath; 

        public DataSaveSystem()
        {
           
            savePath = Path.Combine(Application.persistentDataPath, "playerdata.dat");
        }

        public void SaveData(PlayerData data)
        {
            
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = File.Create(savePath))
            {
                formatter.Serialize(fileStream, data);
            }
        }

        public PlayerData LoadData()
        {
            if (File.Exists(savePath))
            {
               
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(savePath, FileMode.Open))
                {
                    return (PlayerData)formatter.Deserialize(fileStream);
                }
            }
            else
            {
                
                return new PlayerData();
            }
        }
    }
}
