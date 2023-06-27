using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class GameplayUIHandler : GameCanvasUI, IGameplayUI
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private ScaleScoreDisplay _scoreDisplay;

        public event Action OnPausePressed;
        
        public void DisplayScore(float score) =>
            _scoreDisplay.DisplayScore(score);

        public void SetHighestScore(float score) =>
            _scoreDisplay.SetHighestScore(score);
        
        protected override void OnAwake()
        {
            _pauseButton.onClick.AddListener(PauseGame);
        }

        private void PauseGame()
        {
            OnPausePressed?.Invoke();
            InvokeTapped();
        }
    }
}