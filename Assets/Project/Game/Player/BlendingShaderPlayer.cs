using UnityEngine;

namespace Project.Game
{
    public class BlendingShaderPlayer : Player, IBlendingShaderPlayer
    {
        private IPlayerBlendingShaderMaintainer _shaderMaintainer;
        private Material _playerMaterial;

        public IPlayerBlendingShaderMaintainer ShaderMaintainer
        {
            get => _shaderMaintainer;
            set => SetShaderMaintainer(value);
        }

        public IObstacleManager ObstaclesSource { get; set; }

        public BlendingShaderPlayer(GameObject playerObject, IPlayerCollisionDetector collisionDetector,
            Material playerMaterial) : 
            base(playerObject, collisionDetector, playerMaterial)
        {
            _playerMaterial = playerMaterial;
            _renderer.material = playerMaterial;
            _renderer.rendererPriority = 1;
            _trailRenderer.rendererPriority = 0;
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
        }

        protected override void OnReset()
        {
            base.OnReset();
            _shaderMaintainer.Reset();
        }

        public override void ApplyTheme(IGameTheme theme)
        {
            _shaderMaintainer.MaintainedShader.PlayerColor = theme.PlayerColor;
            _shaderMaintainer.MaintainedShader.BlendingColor = theme.ObstacleColor;
        }
    }
}