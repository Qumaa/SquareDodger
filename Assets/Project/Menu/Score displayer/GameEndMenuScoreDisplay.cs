using TMPro;
using UnityEngine;

namespace Project.UI
{
    public class GameEndMenuScoreDisplay : MonoBehaviour, IGameScoreDisplay
    {
        [SerializeField] private TextMeshProUGUI _currentScoreLabel;
        [SerializeField] private RectTransform _newRecord;

        private float _highestScore;
        
        public void DisplayScore(float score)
        {
            _currentScoreLabel.text = score.ToString("F0");
            _newRecord.gameObject.SetActive(IsNewRecord(score));
        }

        public void SetHighestScore(float score) =>
            _highestScore = score;

        private bool IsNewRecord(float score) =>
            score >= _highestScore;
    }
}