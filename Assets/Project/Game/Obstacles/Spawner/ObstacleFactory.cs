using UnityEngine;

namespace Project.Game
{
    public class ObstacleFactory : IFactory<IObstacle>
    {
        private readonly GameObject _obstaclePrefab;

        public ObstacleFactory(GameObject obstaclePrefab)
        {
            _obstaclePrefab = obstaclePrefab;
        }

        public IObstacle CreateNew() =>
            GameObject.Instantiate(_obstaclePrefab).GetComponent<Obstacle>();
    }
}