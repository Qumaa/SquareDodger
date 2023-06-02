using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct PlayerFactory : IFactory<IPlayerWithShader>
    {
        private PlayerConfig _playerConfig;
        private IFactory<IPlayerShaderMaintainer> _shaderFactory;

        public PlayerFactory(PlayerConfig playerConfig, IFactory<IPlayerShaderMaintainer> shaderFactory)
        {
            _playerConfig = playerConfig;
            _shaderFactory = shaderFactory;
        }

        public IPlayerWithShader CreateNew()
        {
            var playerObj = Object.Instantiate(_playerConfig.PlayerPrefab);

            var inputService = playerObj.GetComponent<IPlayerInputService>();
            var collisionDetector = playerObj.GetComponent<IPlayerCollisionDetector>();
            var player = new PlayerWithShader(playerObj, collisionDetector)
            {
                InputService = inputService,
                MovementSpeed = _playerConfig.MovementSpeed
            };

            return player;
        }
    }
}