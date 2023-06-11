using UnityEngine;

namespace Project.Game
{
    public interface IPlayerPositionScoreCalculator : IGameScoreCalculator
    {
        Transform PlayerTransform { get; set; }
    }
}