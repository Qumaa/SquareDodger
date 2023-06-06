using UnityEngine;

namespace Project.Architecture
{
    public struct GameBackgroundParticleSystemFactory : IFactory<ParticleSystem>
    {
        private GameObject _prefab;
        private Color32 _particlesColor;

        public GameBackgroundParticleSystemFactory(GameObject prefab, Color32 particlesColor)
        {
            _prefab = prefab;
            _particlesColor = particlesColor;
        }

        public ParticleSystem CreateNew()
        {
            var system = GameObject.Instantiate(_prefab).GetComponent<ParticleSystem>();
            var mainModule = system.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(_particlesColor);
            return system;
        }
    }
}