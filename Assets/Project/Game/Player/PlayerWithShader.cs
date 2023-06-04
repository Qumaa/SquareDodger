using UnityEngine;

namespace Project.Game
{
    public class PlayerWithShader : Player, IPlayerWithShader
    {
        private IPlayerShaderMaintainer _shaderMaintainer;
        private Material _material;
        private float _trailLength;
        private float _trailTime;

        public IPlayerShaderMaintainer ShaderMaintainer
        {
            get => _shaderMaintainer;
            set => SetShaderMaintainer(value);
        }

        public float TrailLength
        {
            get => _trailLength;
            set => SetTrailLength(value);
        }

        public IObstacleManager ObstaclesSource { get; set; }

        public PlayerWithShader(GameObject playerObject, IPlayerCollisionDetector collisionDetector, Material material) : 
            base(playerObject, collisionDetector)
        {
            _material = material;
        }

        public void Update(float timeStep)
        {
            _shaderMaintainer.UpdateBuffer(ObstaclesSource.ActiveObstacles);
        }

        protected override void OnPaused()
        {
            base.OnPaused();
            _trailRenderer.time = float.PositiveInfinity;
        }

        protected override void OnResumed()
        {
            base.OnResumed();
            _trailRenderer.time = _trailTime;
        }

        private void SetShaderMaintainer(IPlayerShaderMaintainer value)
        {
            _shaderMaintainer = value;
            _shaderMaintainer.Material = _material;
        }

        private void SetTrailLength(float length)
        {
            _trailRenderer.time = _trailTime = length / MovementSpeed;
        }
    }
}