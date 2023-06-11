using UnityEngine;

namespace Project.Game
{
    public class PlayerPositionScoreCalculator : IPlayerPositionScoreCalculator
    {
        public Transform PlayerTransform { get; set; }

        public float CalculateScore()
        {
            return Vector2.Dot(PlayerTransform.position, new Vector2(0.707f, 0.707f));
        }
    }
}