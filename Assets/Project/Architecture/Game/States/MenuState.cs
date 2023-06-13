using Project.UI;

namespace Project.Architecture
{
    public class MenuState : GameState
    {
        private IMainMenu _mainMenu;
        private readonly ISettingsMenuOpener _settingsOpener;
        private readonly IApplicationQuitter _applicationQuitter;

        public MenuState(IGameStateMachine stateMachine, IGame game, ISettingsMenuOpener settingsOpener, IApplicationQuitter applicationQuitter) : base(stateMachine, game)
        {
            _settingsOpener = settingsOpener;
            _applicationQuitter = applicationQuitter;
        }

        public override void Enter()
        {
            _game.Gameplay.Pause();
            
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
            _applicationQuitter.Quit();
        }

        private void HandleSettings()
        {
            _settingsOpener.OpenSettings();
        }

        private void GetMenuIfNecessary() =>
            _mainMenu ??= _game.UI.Get<IMainMenu>();
    }
}