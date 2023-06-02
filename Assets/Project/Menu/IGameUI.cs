using UnityEngine;

namespace Project.UI
{
    public interface IGameUI
    {
        void Show();
        void Hide();
        void SetCamera(Camera uiCamera);
    }
}
