using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class GameInputServiceFactory : IFactory<IGameInputService>
    {
        private MonoBehaviour _componentWielder;

        public GameInputServiceFactory(MonoBehaviour componentWielder)
        {
            _componentWielder = componentWielder;
        }

        public IGameInputService CreateNew()
        {
            var service = _componentWielder.GetComponent<IGameInputService>();
            return service;
        }
    }
}