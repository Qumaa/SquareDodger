using DG.Tweening;
using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class BootstrapState : GameState
    {
        private IDisposer _disposer;
        private GameRuntimeData _gameData;
        private Camera _controlledCamera;

        public BootstrapState(IGameStateMachine stateMachine, IGame game, IDisposer disposer, GameRuntimeData gameData,
            Camera controlledCamera) : base(stateMachine, game)
        {
            _disposer = disposer;
            _gameData = gameData;
            _controlledCamera = controlledCamera;
        }

        public override void Enter()
        {
            Initialize();
            CreateGameplay();
            MoveNext();
        }

        public override void Exit()
        {
        }

        private void Initialize()
        {
            Application.targetFrameRate = 60;
            DOTween.Init();
            _game.CameraController = CreateCameraController();
            _game.GameSounds = CreateGameSounds();
            _game.OnLocaleChanged += GameLocalization.SetLocale;
        }

        private void CreateGameplay() =>
            _game.Gameplay = new PausedGameplayFactory(_game, _gameData, _disposer).CreateNew();

        private void MoveNext() =>
            _stateMachine.SetState<InitializeUIState>();

        private ICameraController CreateCameraController()
        {
            var controller = new CameraController(_controlledCamera, _gameData.GameCameraData.ViewportDepth)
            {
                WidthInUnits = _gameData.GameCameraData.ViewportWidth
            };

            return controller;
        }

        private IGameSounds CreateGameSounds()
        {
            var factory = new GameSoundsFactory();
            var sounds = factory.CreateNew();

            return sounds;
        }
    }
}