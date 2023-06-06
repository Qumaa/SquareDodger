using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.BACKGROUND_CONFIG, fileName = "New Background Config")]
    public class GameBackgroundConfig : ScriptableObject
    {
        [SerializeField] private ParticleSystem _backgroundParticlesPrefab;
        [SerializeField] private Vector2 _particlesAreaExtraSize;
        [SerializeField] private float _density;
        [SerializeField] private Vector2 _densityReferenceSize = Vector2.one;

        public GameObject BackgroundParticlesPrefab => _backgroundParticlesPrefab.gameObject;

        public Vector2 ParticlesAreaExtraSize => _particlesAreaExtraSize;

        public float DensityPerUnit => _density / (_densityReferenceSize.x * _densityReferenceSize.y);
    }
}