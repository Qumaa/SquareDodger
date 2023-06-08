using UnityEngine;

namespace Project.UI
{
    public interface IGameCanvasUIRenderer
    {
        void SetCamera(Camera camera);
        void AddUI(IGameCanvasUI ui);
    }
}