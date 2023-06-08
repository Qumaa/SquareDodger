using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct PlayerWithShaderFactory : IFactory<IPlayerWithShader>
    {
        private PlayerRuntimeData _playerData;

        private const string _TRAIL_COLOR_PROPERTY_NAME = "_Color";

        public PlayerWithShaderFactory(PlayerRuntimeData playerData)
        {
            _playerData = playerData;
        }

        public IPlayerWithShader CreateNew()
        {
            var playerObj = Object.Instantiate(_playerData.PlayerPrefab);

            var inputService = playerObj.GetComponent<IPlayerInputService>();
            var collisionDetector = playerObj.GetComponent<IPlayerCollisionDetector>();
            
            _playerData.TrailMaterial.SetColor(_TRAIL_COLOR_PROPERTY_NAME, _playerData.ShaderData.PlayerColor);

            var player = new PlayerWithShader(playerObj, collisionDetector, _playerData.TrailMaterial, 
                _playerData.PlayerMaterial)
            {
                InputService = inputService,
                MovementSpeed = _playerData.MovementSpeed,
                TrailLength = _playerData.TrailLength
            };

            return player;
        }
    }
}