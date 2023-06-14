using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public struct SettingsMenuFactory : IFactory<ISettingsMenu>
    {
        private GameObject _prefab;

        public SettingsMenuFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public ISettingsMenu CreateNew()
        {
            var menu = GameObject.Instantiate(_prefab).GetComponent<ISettingsMenu>();
            return menu;
        }
    }
}