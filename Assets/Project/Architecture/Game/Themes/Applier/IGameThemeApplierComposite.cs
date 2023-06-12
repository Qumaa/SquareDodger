namespace Project.Game
{
    public interface IGameThemeApplierComposite : IGameThemeApplier, IInstanceContainer<IGameThemeAppender>
    {
        
    }
}