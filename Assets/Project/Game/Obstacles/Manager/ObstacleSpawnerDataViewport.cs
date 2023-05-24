namespace Project
{
    public record ObstacleSpawnerDataViewport
    {
        private ObstacleSpawnerConfigViewport _config;
        private ObstaclePooler _pooler;
        private ObstacleFactory _factory;
        private IObstacleSpawnerDataCalculator _calculator;

        public ObstacleSpawnerConfigViewport Config => _config;
        public ObstaclePooler Pooler => _pooler;
        public ObstacleFactory Factory => _factory;
        public IObstacleSpawnerDataCalculator Calculator => _calculator;

        public ObstacleSpawnerDataViewport(ObstacleSpawnerConfigViewport config, ObstaclePooler pooler,
            ObstacleFactory factory, IObstacleSpawnerDataCalculator calculator)
        {
            _config = config;
            _pooler = pooler;
            _factory = factory;
            _calculator = calculator;
        }
    }
}