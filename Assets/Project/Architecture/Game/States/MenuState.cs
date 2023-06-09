using Project.UI;

namespace Project.Architecture
{
    public class MenuState : GameState
    {
        private IMainMenu _mainMenu;

        public MenuState(IGameStateMachine stateMachine, IGame game) : base(stateMachine, game)
        {
        }

        public override void Enter()
        {
            var menu = GetMenu();
            menu.Show();
            menu.OnGameStartPressed += HandleGameStart;
        }

        public override void Exit()
        {
            var menu = GetMenu();
            menu.Hide();
            menu.OnGameStartPressed -= HandleGameStart;
        }

        private void HandleGameStart()
        {
            _stateMachine.SetState<GameLoopState>();
        }

        private IMainMenu GetMenu() =>
            _mainMenu ??= _game.GameCanvasUI.Get<IMainMenu>();
    }
}