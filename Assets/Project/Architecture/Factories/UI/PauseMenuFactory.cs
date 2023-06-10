using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public struct PauseMenuFactory : IFactory<IPauseMenu>
    {
        private GameObject _prefab;
        
        public PauseMenuFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public IPauseMenu CreateNew()
        {
            var menu = GameObject.Instantiate(_prefab).GetComponent<IPauseMenu>();
            return menu;
        }
    }
}