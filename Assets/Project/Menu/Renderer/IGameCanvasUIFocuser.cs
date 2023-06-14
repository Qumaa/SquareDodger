namespace Project.UI
{
    public interface IGameCanvasUIFocuser
    {
        void SetFocus(IGameCanvasUI ui);
        void Unfocus();
    }
}