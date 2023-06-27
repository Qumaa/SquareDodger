using UnityEngine;

namespace Project.Game
{
    public class ObstacleViewportSpawnerRuntimeData : ILoadableFrom<ObstacleViewportSpawnerConfig>
    {
        public int ObstaclesToSpawn { get; private set; }
        public float SpawnInterval { get; private set; }
        public float ObstaclesSpeed { get; private set; }
        public Vector2 ViewportFrom { get; private set; }
        public Vector2 ViewportTo { get; private set; }
        public Vector2 Offset { get; private set; }
        
        public void Load(ObstacleViewportSpawnerConfig data)
        {
            ObstaclesToSpawn = data.ObstaclesToSpawn;
            SpawnInterval = data.SpawnInterval;
            ObstaclesSpeed = data.ObstaclesSpeed;
            ViewportFrom = data.ViewportFrom;
            ViewportTo = data.ViewportTo;
            Offset = data.Offset;
        }
    }
}