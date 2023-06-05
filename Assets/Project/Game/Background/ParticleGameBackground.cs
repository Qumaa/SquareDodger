using UnityEngine;

namespace Project.Game
{
    public class ParticleGameBackground : IParticleGameBackground
    {
        private ParticleSystem _particleSystem;
        private ParticleSystem.Particle[] _particles;
        private Vector2 _centerPosition;

        public Vector2 CenterPosition
        {
            get => _centerPosition;
            set => SetCenterPosition(value);
        }

        public Vector2 Size { get; set; }


        public ParticleGameBackground(ParticleSystem particleSystem)
        {
            _particleSystem = particleSystem;
        }

        public void Update(float timeStep)
        {
            var activeParticles = GetActiveParticlesCount();

            var halfSize = Size / 2f;
            for (var i = 0; i < activeParticles; i++)
                UpdateParticlePosition(i, halfSize);

            SetUpdatedParticles(activeParticles);
        }

        private int GetActiveParticlesCount()
        {
            ReallocateParticlesBufferIfNeeded();
            var particlesCount = _particleSystem.GetParticles(_particles);
            return particlesCount;
        }

        private void UpdateParticlePosition(int particleIndex, Vector2 halfSize)
        {
            _particles[particleIndex].position = CalculateParticlePosition(_particles[particleIndex], halfSize);
        }

        private void SetUpdatedParticles(int activeParticles)
        {
            _particleSystem.SetParticles(_particles, activeParticles);
        }

        private Vector2 CalculateParticlePosition(ParticleSystem.Particle particle, Vector2 halfSize)
        {
            var localSpace = _particleSystem.transform.InverseTransformPoint(particle.position);

            localSpace.x = Mathf.Repeat(localSpace.x + halfSize.x, Size.x) - halfSize.x;
            localSpace.y = Mathf.Repeat(localSpace.y + halfSize.y, Size.y) - halfSize.y;

            return _particleSystem.transform.TransformPoint(localSpace);
        }

        private void SetCenterPosition(Vector2 position)
        {
            _centerPosition = position;
            _particleSystem.transform.position = position;
        }

        private void ReallocateParticlesBufferIfNeeded()
        {
            var max = _particleSystem.main.maxParticles;
            if (_particles == null || _particles.Length < max)
                _particles = new ParticleSystem.Particle[max];
        }

        public void Pause()
        {
            _particleSystem.Pause();
        }

        public void Resume()
        {
            _particleSystem.Play();
        }

        public void Reset()
        {
            _particleSystem.Clear();
        }
    }
}