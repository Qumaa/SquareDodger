using System.Linq;
using UnityEngine;

namespace Project.Game
{
    public class PlayerWithShader : Player, IPlayerWithShader
    {
        private IPlayerBlendingShaderMaintainer _shaderMaintainer;
        private Material _playerMaterial;

        public IPlayerBlendingShaderMaintainer ShaderMaintainer
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
            
            _shaderMaintainer.UpdateShader(ObstaclesSource.ActiveObstacles
                .Where(ObstacleBlendsPlayer)
                .ToArray());
        }

        private bool ObstacleBlendsPlayer(IObstacle obstacle)
        {
            var distance = Vector2.Distance(obstacle.Position, Transform.position);
            var blendingRadius = _shaderMaintainer.MaintainedShader.TotalBlendingRadius;
            
            return distance <= blendingRadius;
        }

        private void SetShaderMaintainer(IPlayerBlendingShaderMaintainer value)
        {
            _shaderMaintainer = value;
            _shaderMaintainer.MaintainedShader.Material = _playerMaterial;
        }

        protected override void OnReset()
        {
            base.OnReset();
            _shaderMaintainer.Reset();
        }
    }
}