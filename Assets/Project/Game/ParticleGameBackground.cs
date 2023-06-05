using UnityEngine;

namespace Project.Game
{
    public class ParticleGameBackground : IParticleGameBackground
    {
        private ParticleSystem _particleSystem;
        private ParticleSystem.Particle[] _particles;

        public Vector2 CenterPosition { get; set; }
        public Vector2 Size { get; set; }


        public ParticleGameBackground(ParticleSystem particleSystem)
        {
            _particleSystem = particleSystem;
        }

        public void Update(float timeStep)
        {
            var activeParticles = GetActiveParticlesCount();
            for (var i = 0; i < activeParticles; i++)
            {
                UpdateParticlePosition(_particles[i], timeStep);
            }
        }

        private int GetActiveParticlesCount()
        {
            ReallocateParticlesBufferIfNeeded();
            var particlesCount = _particleSystem.GetParticles(_particles);
            return particlesCount;
        }

        private void ReallocateParticlesBufferIfNeeded()
        {
            var max = _particleSystem.main.maxParticles;
            if (_particles == null || _particles.Length < max)
                _particles = new ParticleSystem.Particle[max];
        }

        private void UpdateParticlePosition(ParticleSystem.Particle particle, float timeStep)
        {
            particle.position += new Vector3(timeStep, 0);
        }

        public void Pause()
        {
        }

        public void Resume()
        {
        }

        public void Reset()
        {
        }
    }
}