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
        public GameObject UI;
        public GameObject PauseMenu;
        private bool settingsOpen = false;
       
    
        private void Start()
        {
            mainMenu = FindObjectOfType<MainMenuHandler>();

            mainMenu.OnOpenSettingsPressed += Settings;
            mainMenu.OnGameStartPressed += StartGame;
            mainMenu.OnApplicationQuitPressed += QuitGame;
            mainMenu.OnBackMenuPressed += ReturnToMenu;
            mainMenu.OnMenuPressed += BackToMenu;
            mainMenu.OnGameReturnPressed += BackToGame;
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

        private void BackToMenu()
        {
            Debug.Log("Вернулся в меню");
            UI.SetActive(true);
            PauseMenu.SetActive(false);
            
            
        }
        private void BackToGame()
        {
           Debug.Log("Я вернулся в игру");
          
        }

    }
}
