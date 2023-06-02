using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace Project
{






    
        public class PlayButtonHandler : MonoBehaviour
        {
            
            private Button playButton;
            private Button settingsButton;
            private Button exitButton;
            private VisualElement root;
            private UIDocument uiDocument;
            

            private void Start()
            {
               
                UIDocument uiDocument = GetComponent<UIDocument>();
                root = uiDocument.rootVisualElement;
               
                playButton = root.Q<Button>("play");
                playButton.clickable.clicked += StartGame;
                exitButton = root.Q<Button>("exit");
                exitButton.clickable.clicked += ExitGame;
                settingsButton = root.Q<Button>("settings");
                settingsButton.clickable.clicked += OpenSettings;
            }

            private void StartGame()
            {
            
                Debug.Log("Игра начинается!");
            }

            private void ExitGame()
            {
                Debug.Log("Зачем ты вышел?");
            }

            private void OpenSettings()
            {
                Debug.Log("Ха ха ха ты открыл пустоту");
            }
        }




}

