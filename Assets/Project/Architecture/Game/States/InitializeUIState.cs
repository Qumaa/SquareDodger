using Project.Game;
using Project.UI;

namespace Project.Architecture
{
    public class InitializeUIState : GameState
    {
        private GameUIRuntimeData _uiData;
        private ISettingsMenuOpener _opener;
        private readonly PlayerSettingsSavingSystem _savingSystem;
        private PlayerSettingsMaintainer _settingsMaintainer;

        public InitializeUIState(IGameStateMachine stateMachine, IGame game, GameUIRuntimeData uiData, 
            ISettingsMenuOpener opener, PlayerSettingsSavingSystem savingSystem) : 
            base(stateMachine, game)
        {
            _uiData = uiData;
            _opener = opener;
            _savingSystem = savingSystem;
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
            _game.UI = new CanvasUIRendererFactory(_uiData.UICanvasPrefab, _uiData.DarkeningPrefab, _game.GameSounds).CreateNew();
            
            var mainMenu = new MainMenuFactory(_uiData.MainMenuPrefab).CreateNew();
            _game.UI.Add(mainMenu);

            var gameEnd = new GameEndMenuFactory(_uiData.GameEndMenuPrefab).CreateNew();
            gameEnd.Hide();
            _game.UI.Add(gameEnd);

            var pauseMenu = new PauseMenuFactory(_uiData.PauseMenuPrefab).CreateNew();
            pauseMenu.Hide();
            _game.UI.Add(pauseMenu);

            var gameplayUi = new GameplayUIFactory(_uiData.GameplayUIPrefab).CreateNew();
            gameplayUi.Hide();
            _game.UI.Add(gameplayUi);

            var settingsMenu = new SettingsMenuFactory(_uiData.SettingsMenuPrefab).CreateNew();
            settingsMenu.Hide();
            _game.UI.Add(settingsMenu);
            _settingsMaintainer = new PlayerSettingsMaintainer(_savingSystem, settingsMenu, _game);

            _opener.Focuser = _game.UI;
            _opener.SettingsMenu = settingsMenu;
        }

        private void MoveNext()
        {
            _stateMachine.SetState<MenuState>();
        }
    }
}