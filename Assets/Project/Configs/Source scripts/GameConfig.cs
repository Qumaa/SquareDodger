using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.GAME_CONFIG, fileName = "New Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private GameCameraConfig _cameraConfig;
        [SerializeField] private ObstacleManagerConfig _managerConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private GameBackgroundConfig _gameBackgroundConfig;
        [SerializeField] private GameUIConfig _gameUIConfig;
        

        public GameCameraConfig CameraConfig => _cameraConfig;

        public ObstacleManagerConfig ManagerConfig => _managerConfig;

        public PlayerConfig PlayerConfig => _playerConfig;

        public GameBackgroundConfig GameBackgroundConfig => _gameBackgroundConfig;

        public GameUIConfig GameUIConfig => _gameUIConfig;
    }
}
