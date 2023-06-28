using System;
using Project.Architecture;

namespace Project.Game
{
    public class Gameplay : PausableAndResettable, IGameplay
    {
        private IGameCamera _gameCamera;
        private IObstacleManager _obstacleManager;
        private IAnimatedGameFinisher _gameFinisher;
        private IGameBackground _gameBackground;
        private IGameScoreCalculator _scoreCalculator;
        private IGameSounds _gameSounds;

        private IPausableAndResettable[] _gameComposites;
        
        public event Action OnEnded;
        public IBlendingShaderPlayer Player { get; }

        public float Score => _scoreCalculator.CalculateScore();

        public Gameplay(IBlendingShaderPlayer player, IGameCamera gameCamera, IObstacleManager obstacleManager, 
            IAnimatedGameFinisher gameFinisher, IGameBackground gameBackground, IGameScoreCalculator scoreCalculator,
            IGameSounds gameSounds)
        {
            _gameCamera = gameCamera;
            Player = player;
            _obstacleManager = obstacleManager;
            _gameFinisher = gameFinisher;
            _gameBackground = gameBackground;
            _scoreCalculator = scoreCalculator;
            _gameSounds = gameSounds;

            InitializeComposites();
            InitializeSounds();
            SetFinisher();
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
            Player.Update(timeStep);
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
            _gameFinisher.Reset();
            ForeachComposite(x => x.Reset());
        }

        private void InitializeComposites()
        {
            _gameComposites = new IPausableAndResettable[]
            {
                Player,
                _gameCamera,
                _obstacleManager,
                _gameBackground
            };
        }

        private void InitializeSounds()
        {
            _gameSounds.PlayMusicInLoop();
            Player.OnTurned += _gameSounds.PlayTurnSound;
        }

        private void SetFinisher()
        {
            _gameFinisher.GameToFinish = this;
            Player.OnDied += End;
        }

        private void End()
        {
            OnEnded?.Invoke();
            _gameSounds.PlayLoseSound();
            _gameFinisher.Finish();
        }

        private void ForeachComposite(Action<IPausableAndResettable> action)
        {
            for (var i = 0; i < _gameComposites.Length; i++)
            {
                var composite = _gameComposites[i];
                action(composite);
            }
        }
    }
}