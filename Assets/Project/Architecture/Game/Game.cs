using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class Game : IGame
    {
        private IGameCamera _gameCamera;
        private IPlayer _player;
        private IPlayerShaderMaintainer _shaderMaintainer;
        private IObstacleManager _obstacleManager;

        public Game(IPlayer player, IPlayerShaderMaintainer shaderMaintainer, IGameCamera gameCamera,
            IObstacleManager obstacleManager)
        {
            _gameCamera = gameCamera;
            _player = player;
            _shaderMaintainer = shaderMaintainer;
            _obstacleManager = obstacleManager;
        }

        public void FixedUpdate()
        {
            _gameCamera.Update(Time.deltaTime);
        }

        public void Update()
        {
            _obstacleManager.Update(Time.deltaTime);
            _shaderMaintainer.UpdateBuffer(_obstacleManager.ActiveObstacles);
        }

        public void Pause()
        {
            Debug.Log("pause");
        }

        public void Resume()
        {
            Debug.Log("resume");
        }

        public void Finish()
        {
            Debug.Log("finish");
        }
    }
}