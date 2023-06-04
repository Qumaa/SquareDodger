using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    public abstract class CanvasHandlerBase : MonoBehaviour,IGameUI
    {
        protected Canvas canvas;
        public abstract void Hide();
        public abstract void Show();
        public abstract void SetCamera(Camera uiCamera);
        public void SetCamera(Camera uiCamera, float planeDistance)
        {
            if (canvas == null)
                canvas = GetComponent<Canvas>();

            canvas.worldCamera = uiCamera;
            canvas.planeDistance = planeDistance;
        }
        
    }
}
