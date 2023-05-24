using UnityEngine;

namespace Project
{
    public class ObstacleFactory : IFactory<Obstacle>
    {
        private readonly GameObject _obstaclePrefab;

        public ObstacleFactory(GameObject obstaclePrefab)
        {
            _obstaclePrefab = obstaclePrefab;
        }

        public Obstacle CreateNew() =>
            GameObject.Instantiate(_obstaclePrefab).GetComponent<Obstacle>();
    }
}