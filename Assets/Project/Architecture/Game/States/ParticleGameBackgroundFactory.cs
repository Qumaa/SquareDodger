using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct ParticleGameBackgroundFactory : IFactory<IParticleGameBackground>
    {
        private IFactory<ParticleSystem> _particleFactory;
        private float _size;

        public ParticleGameBackgroundFactory(IFactory<ParticleSystem> particleFactory, float size)
        {
            _particleFactory = particleFactory;
            _size = size;
        }

        public IParticleGameBackground CreateNew()
        {
            var background = new ParticleGameBackground(_particleFactory.CreateNew());
            background.Size = new Vector2(_size, _size);
            return background;
        }
    }
}