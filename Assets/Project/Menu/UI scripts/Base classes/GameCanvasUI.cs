using System;
using UnityEngine;

namespace Project.UI
{
    public abstract class GameCanvasUI : MonoBehaviour, IGameCanvasUI
    {
        private RectTransform _transform;
        protected bool _visible => _transform.gameObject.activeSelf;

        private void Awake()
        {
            _transform = (RectTransform) transform;
            OnAwake();
        }

        public event Action OnShouldPlayTappedSound;

        public virtual void Show()
        {
            _transform.gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            _transform.gameObject.SetActive(false);
        }

        public void SetCanvas(Canvas canvas)
        {
            _transform.SetParent(canvas.transform, false);
        }

        public void SetAsFocused()
        {
            _transform.SetAsLastSibling();
        }

        protected void InvokeTapped() =>
            OnShouldPlayTappedSound?.Invoke();

        protected abstract void OnAwake();
    }
}