using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.PLAYER_CONFIG, fileName = "New Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private Material _playerMaterial;
        [SerializeField] private float _trailLength;
        

        public GameObject PlayerPrefab => _playerPrefab;

        public float MovementSpeed => _movementSpeed;

        public Material PlayerMaterial => _playerMaterial;

        public float TrailLength => _trailLength;
    }
}