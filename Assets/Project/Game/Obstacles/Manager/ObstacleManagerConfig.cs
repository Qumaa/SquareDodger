using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.OBSTACLE_MANAGER_CONFIG, fileName = "New Obstacle Manager Config")]
    public class ObstacleManagerConfig : ScriptableObject
    {
        [SerializeField] private Obstacle _obstaclePrefab;
        
        [SerializeField] private ObstacleSpawnerConfigViewport _topSpawnerConfig;
        [SerializeField] private ObstacleSpawnerConfigViewport _leftSpawnerConfig;
        [SerializeField] private ObstacleSpawnerConfigViewport _rightSpawnerConfig;

        public Obstacle ObstaclePrefab => _obstaclePrefab;

        public ObstacleSpawnerConfigViewport TopSpawnerConfig => _topSpawnerConfig;

        public ObstacleSpawnerConfigViewport LeftSpawnerConfig => _leftSpawnerConfig;

        public ObstacleSpawnerConfigViewport RightSpawnerConfig => _rightSpawnerConfig;
    }
}