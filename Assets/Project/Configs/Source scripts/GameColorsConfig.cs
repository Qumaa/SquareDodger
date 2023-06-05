using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.GAME_COLORS_CONFIG, fileName = "New Game Colors Config")]
    public class GameColorsConfig : ScriptableObject
    {
        [SerializeField] private Color _playerColor;
        [SerializeField] private Color _obstaclesColor;
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Color _backgroundParticlesColor;
    }
}