using UnityEngine;

namespace Project.Game
{
    public class BlendingShaderPlayer : Player, IBlendingShaderPlayer
    {
        private IBlendingShaderMaintainer _playerShaderMaintainer;
        private IBlendingShaderMaintainer _trailShaderMaintainer;

        public IBlendingShaderMaintainer PlayerShaderMaintainer
        {
            set => _playerShaderMaintainer = value;
        }

        public IBlendingShaderMaintainer TrailShaderMaintainer
        {
            set => _trailShaderMaintainer = value;
        }

        public IObstacleManager ObstaclesSource { get; set; }

        public BlendingShaderPlayer(GameObject playerObject, IPlayerCollisionDetector collisionDetector,
            Material playerMaterial, Material trailMaterial) : 
            base(playerObject, collisionDetector)
        {
            _renderer.material = playerMaterial;
            _trailRenderer.material = trailMaterial;
        }

        public void Update(float timeStep)
        {
            if (_isPaused)
                return;
            
            _playerShaderMaintainer.UpdateShader(ObstaclesSource.ActiveObstacles);
            _trailShaderMaintainer.UpdateShader(ObstaclesSource.ActiveObstacles);
        }

        public void SetShaderMode(ShaderBlendingMode mode)
        {
            _playerShaderMaintainer.MaintainedShader.BlendingMode = mode;
            _trailShaderMaintainer.MaintainedShader.BlendingMode = mode;
        }

        protected override void OnReset()
        {
            base.OnReset();
            _playerShaderMaintainer.Reset();
            _trailShaderMaintainer.Reset();
        }

        public override void ApplyTheme(IGameTheme theme)
        {
            SetMaintainerTheme(_trailShaderMaintainer, theme);
            SetMaintainerTheme(_playerShaderMaintainer, theme);
        }

        private static void SetMaintainerTheme(IBlendingShaderMaintainer maintainer, IGameTheme theme)
        {
            maintainer.MaintainedShader.PlayerColor = theme.PlayerColor;
            maintainer.MaintainedShader.BlendingColor = theme.ObstacleColor;
        }
    }
}