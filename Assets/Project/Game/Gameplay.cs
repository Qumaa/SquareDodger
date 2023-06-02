using System;
using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class Gameplay : PausableAndResettable, IGameplay
    {
        private IGameCamera _gameCamera;
        private IPlayer _player;
        private IPlayerShaderMaintainer _shaderMaintainer;
        private IObstacleManager _obstacleManager;

        private IPausableAndResettable[] _gameComposites;

        public Gameplay(IPlayer player, IPlayerShaderMaintainer shaderMaintainer, IGameCamera gameCamera,
            IObstacleManager obstacleManager)
        {
            _gameCamera = gameCamera;
            _player = player;
            _shaderMaintainer = shaderMaintainer;
            _obstacleManager = obstacleManager;

            _gameComposites = new IPausableAndResettable[]
            {
                _gameCamera,
                _player,
                _shaderMaintainer,
                _obstacleManager
            };
        }

        public void FixedUpdate(float fixedTimeStep)
        {
            if (_isPaused)
                return;
            
            _gameCamera.FixedUpdate(Time.deltaTime);
        }

        public void Update(float timeStep)
        {
            if (_isPaused)
                return;
            
            _obstacleManager.Update(Time.deltaTime);
            _shaderMaintainer.UpdateBuffer(_obstacleManager.ActiveObstacles);
        }

        protected override void OnPaused()
        {
            base.OnPaused();
            ForeachComposite(x => x.Pause());
        }

        protected override void OnResumed()
        {
            base.OnResumed();
            ForeachComposite(x => x.Resume());
        }

        protected override void OnReset()
        {
            base.OnResumed();
            ForeachComposite(x => x.Reset());
        }

        private void ForeachComposite(Action<IPausableAndResettable> action)
        {
            foreach (var composite in _gameComposites)
                action(composite);
        }
    }
}