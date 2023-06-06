using UnityEngine;

namespace Project.Game
{
    public class PlayerWithShader : Player, IPlayerWithShader
    {
        private IPlayerShaderMaintainer _shaderMaintainer;
        private Material _playerMaterial;

        public IPlayerShaderMaintainer ShaderMaintainer
        {
            get => _shaderMaintainer;
            set => SetShaderMaintainer(value);
        }

        public IObstacleManager ObstaclesSource { get; set; }

        public PlayerWithShader(GameObject playerObject, IPlayerCollisionDetector collisionDetector,
            Material trailMaterial, Material playerMaterial) : 
            base(playerObject, collisionDetector, trailMaterial)
        {
            _playerMaterial = playerMaterial;
            playerObject.GetComponent<Renderer>().material = playerMaterial;
        }

        public void Update(float timeStep)
        {
            if (_isPaused)
                return;
            
            _shaderMaintainer.UpdateBuffer(ObstaclesSource.ActiveObstacles);
        }

        private void SetShaderMaintainer(IPlayerShaderMaintainer value)
        {
            _shaderMaintainer = value;
            _shaderMaintainer.Material = _playerMaterial;
        }
    }
}