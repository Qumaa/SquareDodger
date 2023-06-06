namespace Project.Game
{
    public class ObstacleManagerRuntimeData : ILoadableFrom<ObstacleManagerConfig>
    {
        public Obstacle ObstaclePrefab { get; private set; }
        
        public ObstacleSpawnerConfigViewport RightSpawnerConfig { get; private set; }
        public ObstacleSpawnerConfigViewport TopSpawnerConfig { get; private set; }
        public ObstacleSpawnerConfigViewport LeftSpawnerConfig { get; private set; }
        
        public void Load(ObstacleManagerConfig data)
        {
            ObstaclePrefab = data.ObstaclePrefab;
            RightSpawnerConfig = data.RightSpawnerConfig;
            TopSpawnerConfig = data.TopSpawnerConfig;
            LeftSpawnerConfig = data.LeftSpawnerConfig;
        }
    }
}