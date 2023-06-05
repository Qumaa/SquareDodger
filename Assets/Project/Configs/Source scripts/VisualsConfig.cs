using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.VISUALS_CONFIG, fileName = "New Visuals Config")]
    public class VisualsConfig : ScriptableObject
    {
        [SerializeField] private GameColorsConfig _gameColorsConfig;
        
        [SerializeField] private ParticleSystem _backgroundParticlesPrefab;
        [SerializeField] private float _backgroundParticlesSquareSize;


        public GameColorsConfig GameColorsConfig => _gameColorsConfig;
        public GameObject BackgroundParticlesPrefab => _backgroundParticlesPrefab.gameObject;

        public float BackgroundParticlesSquareSize => _backgroundParticlesSquareSize;
    }
}