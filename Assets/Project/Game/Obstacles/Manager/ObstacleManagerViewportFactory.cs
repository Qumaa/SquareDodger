using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct ObstacleManagerViewportFactory : IFactory<IObstacleManagerViewport>
    {
        private IObstacleDespawnerViewportShader _obstacleDespawner;
        private IFactory<IObstacleSpawner[]> _spawnerFactory;
        private Color32 _obstacleColor;

        public ObstacleManagerViewportFactory(IFactory<IObstacleSpawner[]> spawnerFactory, 
            IObstacleDespawnerViewportShader obstacleDespawner, Color32 obstacleColor)
        {
            _spawnerFactory = spawnerFactory;
            _obstacleDespawner = obstacleDespawner;
            _obstacleColor = obstacleColor;
        }

        public IObstacleManagerViewport CreateNew()
        {
            return new ObstacleManagerViewport(_spawnerFactory.CreateNew(), _obstacleDespawner, _obstacleColor);
        }
    }
}