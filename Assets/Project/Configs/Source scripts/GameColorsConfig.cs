using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.GAME_COLORS_CONFIG, fileName = "New Game Colors Config")]
    public class GameColorsConfig : ScriptableObject
    {
        [SerializeField] private Color32  _playerColor;
        [SerializeField] private Color32 _obstaclesColor;
        [SerializeField] private Color32 _backgroundColor;
        [SerializeField] private Color32 _backgroundParticlesColor;

        public Color32 PlayerColor => _playerColor;

        public Color32 ObstaclesColor => _obstaclesColor;

        public Color32 BackgroundColor => _backgroundColor;

        public Color32 BackgroundParticlesColor => _backgroundParticlesColor;
    }
}