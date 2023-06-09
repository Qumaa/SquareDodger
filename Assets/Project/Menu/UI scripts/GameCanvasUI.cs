using UnityEngine;

namespace Project.UI
{
    public abstract class GameCanvasUI : MonoBehaviour, IGameCanvasUI
    {
        private RectTransform _transform;

        private void Awake()
        {
            _transform = (RectTransform) transform;
            OnAwake();
        }

        public void Show()
        {
            _transform.gameObject.SetActive(true);
        }

        public void Hide()
        {
            _transform.gameObject.SetActive(false);
        }

        public void SetCanvas(Canvas canvas)
        {
            _transform.SetParent(canvas.transform, false);
        }

        protected abstract void OnAwake();
    }
}