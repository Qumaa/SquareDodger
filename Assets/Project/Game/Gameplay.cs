using System;
using Project.Game;

namespace Project.Architecture
{
    public class Gameplay : PausableAndResettable, IGameplay
    {
        private IGameCamera _gameCamera;
        private IPlayerWithShader _player;
        private IObstacleManager _obstacleManager;
        private IGameBackground _gameBackground;

        private IPausableAndResettable[] _gameComposites;

        public Gameplay(IPlayerWithShader player, IGameCamera gameCamera, IObstacleManager obstacleManager, 
            IGameFinisher gameFinisher, IGameBackground gameBackground)
        {
            _gameCamera = gameCamera;
            _player = player;
            _obstacleManager = obstacleManager;
            _gameBackground = gameBackground;

            InitializeComposites();
            InitializeFinisher(gameFinisher);
        }

        public void FixedUpdate(float fixedTimeStep)
        {
            if (_isPaused)
                return;
            
            _gameCamera.FixedUpdate(fixedTimeStep);
            _gameBackground.CenterPosition = _gameCamera.Position;
        }

        public void Update(float timeStep)
        {
            if (_isPaused)
                return;
            
            _obstacleManager.Update(timeStep);
            _player.Update(timeStep);
            _gameBackground.Update(timeStep);
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

        private void InitializeFinisher(IGameFinisher gameFinisher)
        {
            gameFinisher.GameToFinish = this;
            _player.OnDied += gameFinisher.Finish;
        }

        private void InitializeComposites()
        {
            _gameComposites = new IPausableAndResettable[]
            {
                _gameCamera,
                _player,
                _obstacleManager,
                _gameBackground
            };
        }

        private void ForeachComposite(Action<IPausableAndResettable> action)
        {
            foreach (var composite in _gameComposites)
                action(composite);
        }
    }
}