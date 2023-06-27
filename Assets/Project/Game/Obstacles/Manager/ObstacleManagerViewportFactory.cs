using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct ObstacleManagerViewportFactory : IFactory<IObstacleManagerViewport>
    {
        private IObstacleDespawnerViewportShader _obstacleDespawner;
        private IFactory<IObstacleSpawner[]> _spawnerFactory;

        public ObstacleManagerViewportFactory(ObstacleManagerRuntimeData managerConfig, Camera controlledCamera, float cameraViewportDepth)
        {
            var pooler = new ObstaclePooler();
            var factory = new ObstacleFactory(managerConfig.ObstaclePrefab.gameObject);
            
            var despawner = new ObstacleDespawnerViewportShader(controlledCamera,
                new Vector2(0.707f, 0.707f) * managerConfig.ObstaclePrefab.Size, pooler);

            var spawnerFactory = new ObstacleManagerSpawnerFactory(managerConfig, controlledCamera, cameraViewportDepth,
                pooler, factory);
            
            _spawnerFactory = spawnerFactory;
            _obstacleDespawner = despawner;
        }

        public IObstacleManagerViewport CreateNew() =>
            new ObstacleManagerViewport(_spawnerFactory.CreateNew(), _obstacleDespawner);
    }
}