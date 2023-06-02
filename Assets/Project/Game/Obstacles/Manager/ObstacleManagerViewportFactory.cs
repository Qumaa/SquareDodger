namespace Project.Game
{
    public struct ObstacleManagerViewportFactory : IFactory<IObstacleManagerViewport>
    {
        private IObstacleDespawnerViewportShader _obstacleDespawner;

        private IFactory<IObstacleSpawner[]> _spawnerFactory;

        public ObstacleManagerViewportFactory(IFactory<IObstacleSpawner[]> spawnerFactory, IObstacleDespawnerViewportShader obstacleDespawner)
        {
            _spawnerFactory = spawnerFactory;
            _obstacleDespawner = obstacleDespawner;
        }

        public IObstacleManagerViewport CreateNew()
        {
            return new ObstacleManagerViewport(_spawnerFactory.CreateNew(), _obstacleDespawner);
        }
    }
}