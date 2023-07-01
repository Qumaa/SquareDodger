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
        private IGameThemeApplierComposite _themeApplier;

        public BootstrapState(IGameStateMachine stateMachine, IGame game, IDisposer disposer, GameRuntimeData gameData,
            Camera controlledCamera, IGameThemeApplierComposite themeApplier) : base(stateMachine, game)
        {
            _disposer = disposer;
            _gameData = gameData;
            _controlledCamera = controlledCamera;
            _themeApplier = themeApplier;
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
        }

        private void CreateGameplay() =>
            _game.Gameplay = new PausedGameplayFactory(_themeApplier, _game, _gameData, _disposer).CreateNew();

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
            var factory = new GameSoundsFactory(_gameData.GameSoundsData);
            var sounds = factory.CreateNew();

            return sounds;
        }
    }
}