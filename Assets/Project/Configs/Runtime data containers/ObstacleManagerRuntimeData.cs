namespace Project.Game
{
    public class ObstacleManagerRuntimeData : ILoadableFrom<ObstacleManagerConfig>
    {
        public Obstacle ObstaclePrefab { get; private set; }
        
        public ObstacleViewportSpawnerConfig RightSpawnerConfig { get; private set; }
        public ObstacleViewportSpawnerConfig TopSpawnerConfig { get; private set; }
        public ObstacleViewportSpawnerConfig LeftSpawnerConfig { get; private set; }
        
        public void Load(ObstacleManagerConfig data)
        {
            ObstaclePrefab = data.ObstaclePrefab;
            RightSpawnerConfig = data.RightSpawnerConfig;
            TopSpawnerConfig = data.TopSpawnerConfig;
            LeftSpawnerConfig = data.LeftSpawnerConfig;
        }
    }
}