using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Project.Architecture;
using Project.Game;


namespace Project
{
    public class PlayerDataManager : MonoBehaviour, IPlayerDataSaveSystem
    {
        private PlayerData _playerData;
        private DataSaveSystem _dataSaveSystem;
        private const string SaveFileName = "playerData.json";
        [SerializeField] private ResourcesGameThemeResolver _themeManager;

        private void Awake()
        {
            _dataSaveSystem = new DataSaveSystem();
            LoadData();
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SaveData();
            }
        }

        public void SaveData()
        {
             // var data = new PlayerData
            {
                var data = new PlayerData();
                // Тут я в ступоре тоха


                    // data.theme = ResourcesGameThemeResolver.Instance.SwitchThemeType;
                data.theme = ResourcesGameThemeResolver.SwitchThemeType(NashThemes.CurrentTheme);
             
                
                _dataSaveSystem.SaveData(data);


            };

            // _dataSaveSystem.SaveData(data);
        }


        public void SaveData(PlayerData data)
        {
            var json = JsonUtility.ToJson(data);
            var savePath = Path.Combine(Application.persistentDataPath, SaveFileName);
            File.WriteAllText(savePath, json);
            
        }

        PlayerData IPlayerDataSaveSystem.LoadData()
        {
            var savePath = Path.Combine(Application.persistentDataPath, SaveFileName);


            if (File.Exists(savePath))
            {

                var jsonData = File.ReadAllText(savePath);


                var data = JsonUtility.FromJson<PlayerData>(jsonData);

                return data;
            }
            else
            {

                return new PlayerData();
            }
        }

        private void LoadData()
        {
            var data = _dataSaveSystem.LoadData();

            if (data == null) return;
            _playerData = data;

            ResourcesGameThemeResolver.SwitchThemeType(_playerData.theme);

        }

    }
}
