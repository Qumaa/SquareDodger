using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    
    
    public class ButtonHandler : MonoBehaviour
    {
        private MainMenuHandler mainMenu;
        public GameObject SettingsPanels;
    
        private void Start()
        {
            mainMenu = FindObjectOfType<MainMenuHandler>();

            mainMenu.OnOpenSettingsPressed += Settings;
            mainMenu.OnGameStartPressed += StartGame;
            mainMenu.OnApplicationQuitPressed += QuitGame;
            mainMenu.OnBackMenuPressed += ReturnToMenu;
        }
    
        private void StartGame()
        {
            
            Debug.Log("Игра начинается!");
        }
    
        private void QuitGame()
        {
            
            Debug.Log("Выход из игры");
            Application.Quit();
        }

        private void Settings()
        {
            SettingsPanels.SetActive(true);
            Debug.Log("Настройки");
        }

        private void ReturnToMenu()
        {
            SettingsPanels.SetActive(false);
            Debug.Log("Пусти обратно");
        }
    }
}
