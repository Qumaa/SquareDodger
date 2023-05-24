using UnityEngine;

namespace Project
{
    [CreateAssetMenu(menuName = AssetMenuPaths.OBSTACLE_SPAWNER, fileName = "New Obstacle Spawner Config")]
    public class ObstacleSpawnerConfigViewport : ObstacleSpawnerConfig
    {
        [Header("Viewport-specific settings")] 
        [SerializeField] private Vector2 _viewportFrom;
        [SerializeField] private Vector2 _viewportTo;
        [SerializeField] private Vector2 _offset;

        public Vector2 ViewportFrom => _viewportFrom;

        public Vector2 ViewportTo => _viewportTo;

        public Vector2 Offset => _offset;
    }
}