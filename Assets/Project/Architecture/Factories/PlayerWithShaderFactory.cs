using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct PlayerWithShaderFactory : IFactory<IPlayerWithShader>
    {
        private PlayerConfig _playerConfig;
        private Color32 _playerColor;
        private Color32 _blendingColor;

        private const string _PLAYER_COLOR_PROPERTY_NAME = "_PlayerColor";
        private const string _OBSTACLE_COLOR_PROPERTY_NAME = "_ObstacleColor";
        private const string _TRAIL_COLOR_PROPERTY_NAME = "_Color";

        public PlayerWithShaderFactory(PlayerConfig playerConfig, Color32 playerColor, Color32 blendingColor)
        {
            _playerConfig = playerConfig;
            _playerColor = playerColor;
            _blendingColor = blendingColor;
        }

        public IPlayerWithShader CreateNew()
        {
            var playerObj = Object.Instantiate(_playerConfig.PlayerPrefab);

            var inputService = playerObj.GetComponent<IPlayerInputService>();
            var collisionDetector = playerObj.GetComponent<IPlayerCollisionDetector>();
            
            _playerConfig.TrailMaterial.SetColor(_TRAIL_COLOR_PROPERTY_NAME, _playerColor);
            _playerConfig.PlayerMaterial.SetColor(_PLAYER_COLOR_PROPERTY_NAME, _playerColor);
            _playerConfig.PlayerMaterial.SetColor(_OBSTACLE_COLOR_PROPERTY_NAME, _blendingColor);
            
            var player = new PlayerWithShader(playerObj, collisionDetector, _playerConfig.TrailMaterial, _playerConfig.PlayerMaterial)
            {
                InputService = inputService,
                MovementSpeed = _playerConfig.MovementSpeed,
                TrailLength = _playerConfig.TrailLength
            };

            return player;
        }
    }
}