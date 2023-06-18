using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct PlayerWithShaderFactory : IFactory<IBlendingShaderPlayer>
    {
        private PlayerRuntimeData _playerData;
        private IGameInputService _inputService;

        public PlayerWithShaderFactory(PlayerRuntimeData playerData, IGameInputService inputService)
        {
            _playerData = playerData;
            _inputService = inputService;
        }

        public IBlendingShaderPlayer CreateNew()
        {
            var playerObj = Object.Instantiate(_playerData.PlayerPrefab);

            var collisionDetector = playerObj.GetComponent<IPlayerCollisionDetector>();

            var player = new BlendingShaderPlayer(playerObj, collisionDetector, 
                _playerData.PlayerMaterial)
            {
                InputService = _inputService,
                MovementSpeed = _playerData.MovementSpeed,
                TrailLength = _playerData.TrailLength
            };

            return player;
        }
    }
}