using Project.Game;
using Project.UI;

namespace Project.Architecture
{
    public class InitializeUIState : GameState
    {
        private GameUIRuntimeData _uiData;
        private IMainMenu _mainMenu;

        public InitializeUIState(IGameStateMachine stateMachine, IGame game, GameUIRuntimeData uiData) : base(stateMachine, game)
        {
            _uiData = uiData;
        }

        public override void Enter()
        {
            CreateUI(_uiData, _game.InputService);
            _game.GameCanvasUI.SetCamera(_game.CameraController.ControlledCamera);
            _game.GameCanvasUI.Add(_mainMenu);
            MoveNext();
        }

        public override void Exit()
        {
        }

        private void MoveNext()
        {
            _stateMachine.SetState<MenuState>();
        }

        private void CreateUI(GameUIRuntimeData uiData, IGameInputService inputService)
        {
            _game.GameCanvasUI = new CanvasUIRendererFactory(uiData.UICanvasPrefab).CreateNew();
            
            _mainMenu = new MainMenuFactory(uiData.MainMenuPrefab).CreateNew();
            _mainMenu.InputService = inputService;
        }
    }
}