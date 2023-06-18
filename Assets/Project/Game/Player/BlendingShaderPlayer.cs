using UnityEngine;

namespace Project.Game
{
    public class BlendingShaderPlayer : Player, IBlendingShaderPlayer
    {
        private IBlendingShaderMaintainer _playerShaderMaintainer;
        private IBlendingShaderMaintainer _trailShaderMaintainer;
        private Material _playerMaterial;
        private Material _trailMaterial;

        public IBlendingShaderMaintainer PlayerShaderMaintainer
        {
            set => SetPlayerMaintainer(value);
        }

        public IBlendingShaderMaintainer TrailShaderMaintainer
        {
            set => SetTrailMaintainer(value);
        }

        public IObstacleManager ObstaclesSource { get; set; }

        public BlendingShaderPlayer(GameObject playerObject, IPlayerCollisionDetector collisionDetector,
            Material playerMaterial, Material trailMaterial) : 
            base(playerObject, collisionDetector)
        {
            _playerMaterial = playerMaterial;
            _trailMaterial = trailMaterial;
            
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

        private void SetPlayerMaintainer(IBlendingShaderMaintainer maintainer)
        {
            _playerShaderMaintainer = maintainer;
            SetMaintainerMaterial(maintainer, _playerMaterial);
        }
        
        private void SetTrailMaintainer(IBlendingShaderMaintainer maintainer)
        {
            _trailShaderMaintainer = maintainer;
            SetMaintainerMaterial(maintainer, _trailMaterial);
        }

        private static void SetMaintainerMaterial(IBlendingShaderMaintainer maintainer, Material material) =>
            maintainer.MaintainedShader.Material = material;

        private static void SetMaintainerTheme(IBlendingShaderMaintainer maintainer, IGameTheme theme)
        {
            maintainer.MaintainedShader.PlayerColor = theme.PlayerColor;
            maintainer.MaintainedShader.BlendingColor = theme.ObstacleColor;
        }
    }
}