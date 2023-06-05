using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.GAME_CONFIG, fileName = "New Game Config")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private GameCameraConfig _cameraConfig;
        [SerializeField] private ObstacleManagerConfig _managerConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private VisualsConfig _visualsConfig;
        

        public GameCameraConfig CameraConfig => _cameraConfig;

        public ObstacleManagerConfig ManagerConfig => _managerConfig;

        public PlayerConfig PlayerConfig => _playerConfig;

        public VisualsConfig VisualsConfig => _visualsConfig;
    }
}
