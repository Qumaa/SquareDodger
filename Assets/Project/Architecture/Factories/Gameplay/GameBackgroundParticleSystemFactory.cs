using UnityEngine;

namespace Project.Architecture
{
    public struct GameBackgroundParticleSystemFactory : IFactory<ParticleSystem>
    {
        private GameObject _prefab;

        public GameBackgroundParticleSystemFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public ParticleSystem CreateNew()
        {
            var system = GameObject.Instantiate(_prefab).GetComponent<ParticleSystem>();
            return system;
        }
    }
}