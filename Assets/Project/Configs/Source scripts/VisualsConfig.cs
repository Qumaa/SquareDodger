using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.VISUALS_CONFIG, fileName = "New Visuals Config")]
    public class VisualsConfig : ScriptableObject
    {
        [SerializeField] private ParticleSystem _backgroundParticlesPrefab;

        public GameObject BackgroundParticlesPrefab => _backgroundParticlesPrefab.gameObject;
    }
}