using Project.Game;
using Project.UI;
using UnityEngine;

namespace Project.Architecture
{
    public struct CanvasUIRendererFactory : IFactory<IGameCanvasUIRenderer>
    {
        private readonly GameObject _canvasPrefab;
        private readonly RectTransform _focusDarkeningPrefab;
        private readonly IGameSounds _gameSounds;

        public CanvasUIRendererFactory(GameObject canvasPrefab, RectTransform focusDarkeningPrefab, IGameSounds gameSounds)
        {
            _canvasPrefab = canvasPrefab;
            _focusDarkeningPrefab = focusDarkeningPrefab;
            _gameSounds = gameSounds;
        }

        public IGameCanvasUIRenderer CreateNew()
        {
            var canvas = GameObject.Instantiate(_canvasPrefab).GetComponent<Canvas>();
            var darkening = (RectTransform) GameObject.Instantiate(_focusDarkeningPrefab).transform;
            darkening.SetParent(canvas.transform, false);
            var renderer = new GameCanvasUIRenderer(canvas, darkening, _gameSounds);
            return renderer;
        }
    }
}