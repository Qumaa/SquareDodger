namespace Project
{
    public interface IContainer<in TItem>
    {
        void Add<T>(T item) where T : TItem;
        T Get<T>() where T : TItem;
        bool Contains<T>() where T : TItem;
    }
}