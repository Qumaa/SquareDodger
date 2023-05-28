namespace Project.Game
{
    public record ObstacleSpawnerDataViewport
    {
        private ObstacleSpawnerConfigViewport _config;
        private IPooler<IObstacle> _pooler;
        private IFactory<IObstacle> _factory;
        private IObstacleSpawnerDataCalculator _calculator;

        public ObstacleSpawnerConfigViewport Config => _config;
        public IPooler<IObstacle> Pooler => _pooler;
        public IFactory<IObstacle> Factory => _factory;
        public IObstacleSpawnerDataCalculator Calculator => _calculator;

        public ObstacleSpawnerDataViewport(ObstacleSpawnerConfigViewport config, IPooler<IObstacle> pooler,
            IFactory<IObstacle> factory, IObstacleSpawnerDataCalculator calculator)
        {
            _config = config;
            _pooler = pooler;
            _factory = factory;
            _calculator = calculator;
        }
    }
}