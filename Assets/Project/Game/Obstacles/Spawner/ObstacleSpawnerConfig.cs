using UnityEngine;

namespace Project
{
    [CreateAssetMenu(menuName = AssetMenuPaths.ObstacleSpawner, fileName = "New Obstacle Spawner Config")]
    public class ObstacleSpawnerConfig : ScriptableObject
    {
        [SerializeField] private int _obstaclesToSpawn;

        public int ObstaclesToSpawn => _obstaclesToSpawn;
    }
}