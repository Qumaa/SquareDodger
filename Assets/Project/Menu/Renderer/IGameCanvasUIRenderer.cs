using UnityEngine;

namespace Project.UI
{
    public interface IGameCanvasUIRenderer : ISingleContainer<IGameCanvasUI>
    {
        void SetCamera(Camera camera);
    }
}