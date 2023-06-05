using UnityEngine;

namespace Project.Game
{
    public class PlayerWithShader : Player, IPlayerWithShader
    {
        private IPlayerShaderMaintainer _shaderMaintainer;
        private Material _material;

        public IPlayerShaderMaintainer ShaderMaintainer
        {
            get => _shaderMaintainer;
            set => SetShaderMaintainer(value);
        }

        public IObstacleManager ObstaclesSource { get; set; }

        public PlayerWithShader(GameObject playerObject, IPlayerCollisionDetector collisionDetector, Material material) : 
            base(playerObject, collisionDetector)
        {
            _material = material;
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
            _shaderMaintainer.Material = _material;
        }
    }
}