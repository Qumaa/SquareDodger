using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct PlayerWithShaderFactory : IFactory<IPlayerWithShader>
    {
        private PlayerRuntimeData _playerData;
        private Color32 _playerColor;
        private Color32 _blendingColor;

        private const string _PLAYER_COLOR_PROPERTY_NAME = "_PlayerColor";
        private const string _OBSTACLE_COLOR_PROPERTY_NAME = "_ObstacleColor";
        private const string _TRAIL_COLOR_PROPERTY_NAME = "_Color";

        public PlayerWithShaderFactory(PlayerRuntimeData playerData, Color32 playerColor, Color32 blendingColor)
        {
            _playerData = playerData;
            _playerColor = playerColor;
            _blendingColor = blendingColor;
        }

        public IPlayerWithShader CreateNew()
        {
            var playerObj = Object.Instantiate(_playerData.PlayerPrefab);

            var inputService = playerObj.GetComponent<IPlayerInputService>();
            var collisionDetector = playerObj.GetComponent<IPlayerCollisionDetector>();
            
            _playerData.TrailMaterial.SetColor(_TRAIL_COLOR_PROPERTY_NAME, _playerColor);
            _playerData.PlayerMaterial.SetColor(_PLAYER_COLOR_PROPERTY_NAME, _playerColor);
            _playerData.PlayerMaterial.SetColor(_OBSTACLE_COLOR_PROPERTY_NAME, _blendingColor);
            
            var player = new PlayerWithShader(playerObj, collisionDetector, _playerData.TrailMaterial, 
                _playerData.PlayerMaterial, _playerData.BlendingRadius, _playerData.BlendingLength)
            {
                InputService = inputService,
                MovementSpeed = _playerData.MovementSpeed,
                TrailLength = _playerData.TrailLength
            };

            return player;
        }
    }
}