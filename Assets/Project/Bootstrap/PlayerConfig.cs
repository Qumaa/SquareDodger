using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.PLAYER_CONFIG, fileName = "New Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private float _movementSpeed;

        public GameObject PlayerPrefab => _playerPrefab;

        public float MovementSpeed => _movementSpeed;
    }
}