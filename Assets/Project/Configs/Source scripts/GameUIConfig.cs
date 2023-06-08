using Project.UI;
using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.UI_CONFIG, fileName = "New Game UI Config")]
    public class GameUIConfig : ScriptableObject
    {
        [SerializeField] private Canvas _uiCanvas;
        [SerializeField] private MainMenuHandler _mainMenu;

        public GameObject UICanvasPrefab => _uiCanvas.gameObject;
        public GameObject MainMenuPrefab => _mainMenu.gameObject;
    }
}