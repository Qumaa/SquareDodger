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

        public ParticleSystem CreateNew() =>
            GameObject.Instantiate(_prefab).GetComponent<ParticleSystem>();
    }
}