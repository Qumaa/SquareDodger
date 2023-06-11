using Project.UI;
using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.UI_CONFIG, fileName = "New Game UI Config")]
    public class GameUIConfig : ScriptableObject
    {
        [SerializeField] private Canvas _uiCanvas;
        [SerializeField] private MainMenuHandler _mainMenu;
        [SerializeField] private PauseMenuHandler _pauseMenu;
        [SerializeField] private GameEndMenuHandler _gameEndMenu;
        [SerializeField] private GameplayUIHandler _gameplayUI;
        
        public GameObject UICanvasPrefab => _uiCanvas.gameObject;
        public GameObject MainMenuPrefab => _mainMenu.gameObject;
        public GameObject PauseMenuPrefab => _pauseMenu.gameObject;
        public GameObject GameEndMenuPrefab => _gameEndMenu.gameObject;
        public GameObject GameplayUIPrefab => _gameplayUI.gameObject;
    }
}