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
            GetMenuIfNecessary();
            
            _mainMenu.Show();
            _mainMenu.OnGameStartPressed += HandleGameStart;
            _mainMenu.OnApplicationQuitPressed += HandleQuit;
            _mainMenu.OnOpenSettingsPressed += HandleSettings;
        }

        public override void Exit()
        {
            _mainMenu.Hide();
            _mainMenu.OnGameStartPressed -= HandleGameStart;
            _mainMenu.OnApplicationQuitPressed -= HandleQuit;
            _mainMenu.OnOpenSettingsPressed -= HandleSettings;
        }

        private void HandleGameStart()
        {
            _stateMachine.SetState<GameLoopState>();
        }

        private void HandleQuit()
        {
            throw new System.NotImplementedException();
        }

        private void HandleSettings()
        {
            throw new System.NotImplementedException();
        }

        private void GetMenuIfNecessary() =>
            _mainMenu ??= _game.UI.Get<IMainMenu>();
    }
}