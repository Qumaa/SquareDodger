using UnityEngine;

namespace Project.UI
{
    public interface IGameCanvasUIRenderer : IContainer<IGameCanvasUI>
    {
        void SetCamera(Camera camera);
    }
}