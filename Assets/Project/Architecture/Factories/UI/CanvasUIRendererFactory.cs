using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public struct CanvasUIRendererFactory : IFactory<IGameCanvasUIRenderer>
    {
        private GameObject _prefab;

        public CanvasUIRendererFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public IGameCanvasUIRenderer CreateNew()
        {
            var canvas = GameObject.Instantiate(_prefab).GetComponent<Canvas>();
            var renderer = new GameCanvasUIRenderer(canvas);
            return renderer;
        }
    }
}