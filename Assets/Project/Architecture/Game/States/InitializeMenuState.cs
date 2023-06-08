using Project.UI;

namespace Project.Architecture
{
    public class InitializeMenuState : GameState
    {
        private IMainMenu _mainMenu;

        public InitializeMenuState(IGameStateMachine stateMachine, IGame game, IMainMenu mainMenu) : base(stateMachine, game)
        {
            _mainMenu = mainMenu;
        }

        public override void Enter()
        {
            _game.GameCanvasUI.SetCamera(_game.CameraController.ControlledCamera);
            _game.GameCanvasUI.AddUI(_mainMenu);
            _stateMachine.SetState<MenuState>();
        }

        public override void Exit()
        {
        }
    }
}