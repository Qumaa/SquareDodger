using UnityEngine;

namespace Project.UI
{
    public class GameCanvasUIRenderer : IGameCanvasUIRenderer
    {
        private Canvas _canvas;

        public GameCanvasUIRenderer(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void SetCamera(Camera uiCamera)
        {
            _canvas.worldCamera = uiCamera;
            _canvas.planeDistance = 1;
        }

        public void AddUI(IGameCanvasUI ui)
        {
            ui.SetCanvas(_canvas);
        }
    }
}