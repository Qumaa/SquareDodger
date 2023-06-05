using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct ParticleGameBackgroundFactory : IFactory<IParticleGameBackground>
    {
        private IFactory<ParticleSystem> _particleFactory;

        public ParticleGameBackgroundFactory(IFactory<ParticleSystem> particleFactory)
        {
            _particleFactory = particleFactory;
        }

        public IParticleGameBackground CreateNew()
        {
            return new ParticleGameBackground(_particleFactory.CreateNew());
        }
    }
}