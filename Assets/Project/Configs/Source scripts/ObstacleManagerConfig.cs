using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.OBSTACLE_MANAGER_CONFIG, fileName = "New Obstacle Manager Config")]
    public class ObstacleManagerConfig : ScriptableObject
    {
        [SerializeField] private Obstacle _obstaclePrefab;
        
        [SerializeField] private ObstacleViewportSpawnerConfig _topSpawnerConfig;
        [SerializeField] private ObstacleViewportSpawnerConfig _leftSpawnerConfig;
        [SerializeField] private ObstacleViewportSpawnerConfig _rightSpawnerConfig;

        public Obstacle ObstaclePrefab => _obstaclePrefab;

        public ObstacleViewportSpawnerConfig TopSpawnerConfig => _topSpawnerConfig;

        public ObstacleViewportSpawnerConfig LeftSpawnerConfig => _leftSpawnerConfig;

        public ObstacleViewportSpawnerConfig RightSpawnerConfig => _rightSpawnerConfig;
    }
}