using UnityEngine;

namespace Project.UI
{
    public interface IGameCanvasUIRenderer : ISingleContainer<IGameCanvasUI>, IGameCanvasUIFocuser
    {
        void SetCamera(Camera camera);
    }
}