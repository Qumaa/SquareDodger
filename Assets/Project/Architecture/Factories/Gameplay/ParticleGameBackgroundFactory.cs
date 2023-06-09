using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct ParticleGameBackgroundFactory : IFactory<IParticleGameBackground>
    {
        private IFactory<ParticleSystem> _particleFactory;
        private Vector2 _backgroundSize;
        private float _particlesPerUnit;

        public ParticleGameBackgroundFactory(IFactory<ParticleSystem> particleFactory, Vector2 backgroundSize, 
            float particlesPerUnit)
        {
            _particleFactory = particleFactory;
            _backgroundSize = backgroundSize;
            _particlesPerUnit = particlesPerUnit;
        }

        public IParticleGameBackground CreateNew()
        {
            var background = new ParticleGameBackground(_particleFactory.CreateNew());
            background.Size = _backgroundSize;
            background.ParticlesPerUnit = _particlesPerUnit;
            return background;
        }
    }
}