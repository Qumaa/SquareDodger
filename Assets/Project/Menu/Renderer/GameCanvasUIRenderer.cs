using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    public class GameCanvasUIRenderer : IGameCanvasUIRenderer
    {
        private Canvas _canvas;
        private Dictionary<Type, IGameCanvasUI> _uis;

        public GameCanvasUIRenderer(Canvas canvas)
        {
            _canvas = canvas;
            _uis = new Dictionary<Type, IGameCanvasUI>();
        }

        public void SetCamera(Camera uiCamera)
        {
            _canvas.worldCamera = uiCamera;
            _canvas.planeDistance = 1;
        }

        public void Add<T>(T item)
            where T : IGameCanvasUI
        {
            _uis.Add(typeof(T), item);
            item.SetCanvas(_canvas);
        }

        public T Get<T>()
            where T : IGameCanvasUI
        {
            return (T)_uis[typeof(T)];
        }

        public bool Contains<T>()
            where T : IGameCanvasUI
        {
            return _uis.ContainsKey(typeof(T));
        }
    }
}