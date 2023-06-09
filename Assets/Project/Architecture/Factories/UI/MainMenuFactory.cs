using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public struct MainMenuFactory : IFactory<IMainMenu>
    {
        private GameObject _mainMenuPrefab;

        public MainMenuFactory(GameObject mainMenuPrefab)
        {
            _mainMenuPrefab = mainMenuPrefab;
        }

        public IMainMenu CreateNew()
        {
            var mainMenu = GameObject.Instantiate(_mainMenuPrefab).GetComponent<IMainMenu>();
            return mainMenu;
        }
    }
}