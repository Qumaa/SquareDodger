using UnityEngine;

namespace Project.Game
{
    public class PlayerWithShader : Player, IPlayerWithShader
    {
        private IPlayerShaderMaintainer _shaderMaintainer;
        public IPlayerShaderMaintainer ShaderMaintainer
        {
            get => _shaderMaintainer;
            set => SetShaderMaintainer(value);
        }

        public IObstacleManager ObstaclesSource { get; set; }

        public PlayerWithShader(GameObject playerObject, IPlayerCollisionDetector collisionDetector) : 
            base(playerObject, collisionDetector)
        {
        }

        public void Update(float timeStep)
        {
            _shaderMaintainer.UpdateBuffer(ObstaclesSource.ActiveObstacles);
        }

        private void SetShaderMaintainer(IPlayerShaderMaintainer value)
        {
            _shaderMaintainer = value;
            _shaderMaintainer.Material = _gameObject.GetComponent<Renderer>().material;
        }
    }
}