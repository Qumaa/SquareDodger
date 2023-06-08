using Project.UI;

namespace Project.Architecture
{
    public class MenuState : GameState
    {
        private IMainMenu _mainMenu;

        public MenuState(IGameStateMachine stateMachine, IGame game, IMainMenu mainMenu) : base(stateMachine, game)
        {
            _mainMenu = mainMenu;
        }

        public override void Enter()
        {
            _mainMenu.Show();
            _mainMenu.OnGameStartPressed += HandleGameStart;
        }

        public override void Exit()
        {
            _mainMenu.Hide();
        }

        private void HandleGameStart()
        {
            _stateMachine.SetState<GameLoopState>();
        }
    }
}