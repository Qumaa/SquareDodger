using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public struct MainMenuFactory : IFactory<IMainMenu>
    {
        private GameObject _prefab;

        public MainMenuFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public IMainMenu CreateNew() =>
            GameObject.Instantiate(_prefab).GetComponent<IMainMenu>();
    }
}