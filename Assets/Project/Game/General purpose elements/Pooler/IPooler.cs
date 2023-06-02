namespace Project.Game
{
    public interface IPooler<TTarget>
        where TTarget : IPoolerTarget
    {
        void Push(TTarget objToPool);
        bool CanPop();
        TTarget Pop();
    }
}