using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public struct CanvasUIRendererFactory : IFactory<IGameCanvasUIRenderer>
    {
        private GameObject _prefab;
        private RectTransform _focusDarkening;

        public CanvasUIRendererFactory(GameObject prefab, RectTransform focusDarkening)
        {
            _prefab = prefab;
            _focusDarkening = focusDarkening;
        }

        public IGameCanvasUIRenderer CreateNew()
        {
            var canvas = GameObject.Instantiate(_prefab).GetComponent<Canvas>();
            var darkening = (RectTransform) GameObject.Instantiate(_focusDarkening).transform;
            darkening.SetParent(canvas.transform, false);
            var renderer = new GameCanvasUIRenderer(canvas, darkening);
            return renderer;
        }
    }
}