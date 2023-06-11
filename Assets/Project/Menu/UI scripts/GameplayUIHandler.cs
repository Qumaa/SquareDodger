using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class GameplayUIHandler : GameCanvasUI, IGameplayUI
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private TMP_Text _scoreText;
        

        public event Action OnPausePressed;
        
        public void DisplayScore(float score)
        {
            _scoreText.text = score.ToString("F2");
        }

        protected override void OnAwake()
        {
            _pauseButton.onClick.AddListener(PauseGame);
        }

        private void PauseGame()
        {
            OnPausePressed?.Invoke();
        }
    }
}