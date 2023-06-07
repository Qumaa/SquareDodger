using UnityEngine;

namespace Project.Game
{
    public class PlayerWithShader : Player, IPlayerWithShader
    {
        private IPlayerBlendingShaderMaintainer _shaderMaintainer;
        private Material _playerMaterial;
        private readonly float _blendingRadius;
        private readonly float _blendingLength;

        public IPlayerBlendingShaderMaintainer ShaderMaintainer
        {
            get => _shaderMaintainer;
            set => SetShaderMaintainer(value);
        }

        public IObstacleManager ObstaclesSource { get; set; }

        public PlayerWithShader(GameObject playerObject, IPlayerCollisionDetector collisionDetector,
            Material trailMaterial, Material playerMaterial, float blendingRadius, float blendingLength) : 
            base(playerObject, collisionDetector, trailMaterial)
        {
            _playerMaterial = playerMaterial;
            _blendingRadius = blendingRadius;
            _blendingLength = blendingLength;
            playerObject.GetComponent<Renderer>().material = playerMaterial;
        }

        public void Update(float timeStep)
        {
            if (_isPaused)
                return;
            
            _shaderMaintainer.UpdateShader(ObstaclesSource.ActiveObstacles);
        }

        private void SetShaderMaintainer(IPlayerBlendingShaderMaintainer value)
        {
            _shaderMaintainer = value;
            _shaderMaintainer.MaintainedShader.Material = _playerMaterial;
            ResetBlendingValues();
        }

        protected override void OnReset()
        {
            base.OnReset();
            ResetBlendingValues();
        }

        private void ResetBlendingValues()
        {
            _shaderMaintainer.MaintainedShader.BlendingRadius = _blendingRadius;
            _shaderMaintainer.MaintainedShader.BlendingLength = _blendingLength;
        }
    }
}