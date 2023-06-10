using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public struct GameEndMenuFactory : IFactory<IGameEndMenu>
    {
        private GameObject _prefab;
        
        public GameEndMenuFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public IGameEndMenu CreateNew()
        {
            var menu = GameObject.Instantiate(_prefab).GetComponent<IGameEndMenu>();
            return menu;
        }
    }
}