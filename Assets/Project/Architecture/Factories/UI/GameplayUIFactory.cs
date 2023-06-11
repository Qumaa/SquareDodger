using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public struct GameplayUIFactory : IFactory<IGameplayUI>
    {
        private GameObject _prefab;
        
        public GameplayUIFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public IGameplayUI CreateNew()
        {
            var menu = GameObject.Instantiate(_prefab).GetComponent<IGameplayUI>();
            return menu;
        }
    }
}