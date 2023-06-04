using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct PlayerWithShaderFactory : IFactory<IPlayerWithShader>
    {
        private PlayerConfig _playerConfig;

        public PlayerWithShaderFactory(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;
        }

        public IPlayerWithShader CreateNew()
        {
            var playerObj = Object.Instantiate(_playerConfig.PlayerPrefab);

            var inputService = playerObj.GetComponent<IPlayerInputService>();
            var collisionDetector = playerObj.GetComponent<IPlayerCollisionDetector>();
            var player = new PlayerWithShader(playerObj, collisionDetector, _playerConfig.PlayerMaterial)
            {
                InputService = inputService,
                MovementSpeed = _playerConfig.MovementSpeed,
                TrailLength = _playerConfig.TrailLength
            };

            return player;
        }
    }
}