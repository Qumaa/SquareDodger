using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    [CreateAssetMenu(menuName = AssetMenuPaths.GAME_CONFIG, fileName = "New Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private GameCameraConfig _cameraConfig;
        [SerializeField] private ObstacleManagerConfig _managerConfig;
        [SerializeField] private PlayerConfig _playerConfig;

        public GameCameraConfig CameraConfig => _cameraConfig;

        public ObstacleManagerConfig ManagerConfig => _managerConfig;

        public PlayerConfig PlayerConfig => _playerConfig;
    }
}
