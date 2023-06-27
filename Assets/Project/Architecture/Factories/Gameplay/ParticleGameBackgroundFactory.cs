using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct ParticleGameBackgroundFactory : IFactory<IParticleGameBackground>
    {
        private IFactory<ParticleSystem> _particleFactory;
        private ViewportBackgroundSizeCalculator _backgroundSizeCalculator;
        private float _particlesPerUnit;

        public ParticleGameBackgroundFactory(GameRuntimeData gameData, Camera controlledCamera, 
            float particlesPerUnit)
        {

            _particleFactory = new GameBackgroundParticleSystemFactory(
                gameData.GameBackgroundData.BackgroundParticlesPrefab);
            _backgroundSizeCalculator = new ViewportBackgroundSizeCalculator(controlledCamera,
                gameData.GameBackgroundData.ParticlesAreaExtraSize);
            _particlesPerUnit = particlesPerUnit;
        }

        public IParticleGameBackground CreateNew()
        {
            var background = new ParticleGameBackground(_particleFactory.CreateNew(), _backgroundSizeCalculator);
            background.ParticlesPerUnit = _particlesPerUnit;
            return background;
        }
    }
}