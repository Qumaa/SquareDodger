namespace Project.Game
{
    public interface ILoadableFrom<T>
    {
        void Load(T data);
    }
}