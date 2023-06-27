namespace Project.Game
{
    public class ObstacleManagerRuntimeData : ILoadableFrom<ObstacleManagerConfig>
    {
        public Obstacle ObstaclePrefab { get; private set; }
        
        public ObstacleViewportSpawnerRuntimeData RightSpawnerConfig { get; private set; }
        public ObstacleViewportSpawnerRuntimeData TopSpawnerConfig { get; private set; }
        public ObstacleViewportSpawnerRuntimeData LeftSpawnerConfig { get; private set; }
        
        public void Load(ObstacleManagerConfig data)
        {
            ObstaclePrefab = data.ObstaclePrefab;
            
            RightSpawnerConfig = new ObstacleViewportSpawnerRuntimeData();
            RightSpawnerConfig.Load(data.RightSpawnerConfig);

            TopSpawnerConfig = new ObstacleViewportSpawnerRuntimeData();
            TopSpawnerConfig.Load(data.TopSpawnerConfig);
            
            LeftSpawnerConfig = new ObstacleViewportSpawnerRuntimeData();
            LeftSpawnerConfig.Load(data.LeftSpawnerConfig);
        }
    }
}