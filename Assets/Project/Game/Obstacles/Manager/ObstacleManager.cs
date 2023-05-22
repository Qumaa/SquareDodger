using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class ObstacleManager : MonoBehaviour
    {
        [SerializeField] private GameObject _obstaclePrefab;
        
        [Space]
        [SerializeField] private ObstacleSpawnerConfig _topSpawnerConfig;
        [SerializeField] private ObstacleSpawnerConfig _leftSpawnerConfig;
        [SerializeField] private ObstacleSpawnerConfig _rightSpawnerConfig;

        private IObstacleSpawner[] _spawners = new IObstacleSpawner[3];

        private void Start()
        {
            var configs = new[]
            {
                _leftSpawnerConfig,
                _topSpawnerConfig,
                _rightSpawnerConfig
            };
                
            for (int i = 0; i < _spawners.Length; i++)
                _spawners[i] = new ObstacleSpawner(configs[i]);
        }
    }
}
