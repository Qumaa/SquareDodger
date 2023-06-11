using Project.Game;

namespace Project.Architecture
{
    public class InitializeUIState : GameState
    {
        private GameUIRuntimeData _uiData;

        public InitializeUIState(IGameStateMachine stateMachine, IGame game, GameUIRuntimeData uiData) : base(stateMachine, game)
        {
            _uiData = uiData;
        }

        public override void Enter()
        {
            CreateUI();
            SetCamera();
            MoveNext();
        }

        public override void Exit()
        {
        }

        private void SetCamera() =>
            _game.UI.SetCamera(_game.CameraController.ControlledCamera);

        private void CreateUI()
        {
            var inputService = _game.InputService;
            _game.UI = new CanvasUIRendererFactory(_uiData.UICanvasPrefab).CreateNew();
            
            var mainMenu = new MainMenuFactory(_uiData.MainMenuPrefab).CreateNew();
            mainMenu.InputService = inputService;
            _game.UI.Add(mainMenu);

            var gameEnd = new GameEndMenuFactory(_uiData.GameEndMenuPrefab).CreateNew();
            gameEnd.InputService = inputService;
            gameEnd.Hide();
            _game.UI.Add(gameEnd);

            var pauseMenu = new PauseMenuFactory(_uiData.PauseMenuPrefab).CreateNew();
            pauseMenu.InputService = inputService;
            pauseMenu.Hide();
            _game.UI.Add(pauseMenu);

            var gameplayUi = new GameplayUIFactory(_uiData.GameplayUIPrefab).CreateNew();
            gameplayUi.Hide();
            _game.UI.Add(gameplayUi);
        }

        private void MoveNext()
        {
            _stateMachine.SetState<MenuState>();
        }
    }

    
}