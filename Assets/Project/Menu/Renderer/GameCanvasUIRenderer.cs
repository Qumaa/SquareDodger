using System;
using System.Collections.Generic;
using Project.Game;
using UnityEngine;

namespace Project.UI
{
    public class GameCanvasUIRenderer : IGameCanvasUIRenderer
    {
        private readonly Canvas _canvas;
        private readonly RectTransform _focusDarkening;
        private readonly IGameSounds _gameSounds;
        private readonly Dictionary<Type, IGameCanvasUI> _uis;

        public GameCanvasUIRenderer(Canvas canvas, RectTransform focusDarkening, IGameSounds gameSounds)
        {
            _canvas = canvas;
            _focusDarkening = focusDarkening;
            _gameSounds = gameSounds;
            _uis = new Dictionary<Type, IGameCanvasUI>();
            
            Unfocus();
        }

        public void SetCamera(Camera uiCamera)
        {
            _canvas.worldCamera = uiCamera;
            _canvas.planeDistance = 1;
        }

        public void SetFocus(IGameCanvasUI ui)
        {
            _focusDarkening.gameObject.SetActive(true);
            _focusDarkening.SetAsLastSibling();
            ui.SetAsFocused();
        }

        public void Unfocus()
        {
            _focusDarkening.gameObject.SetActive(false);
        }

        public void Add<T>(T item)
            where T : IGameCanvasUI
        {
            _uis.Add(typeof(T), item);
            item.OnShouldPlayTappedSound += _gameSounds.PlayInterfaceTapSound;
            item.SetCanvas(_canvas);
        }

        public T Get<T>()
            where T : IGameCanvasUI
        {
            return (T)_uis[typeof(T)];
        }
    }
}