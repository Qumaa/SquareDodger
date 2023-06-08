using UnityEngine;

namespace Project.UI
{
    public interface IGameCanvasUI 
    {
        void Show();
        void Hide();
        void SetCanvas(Canvas canvas);
    }
}
