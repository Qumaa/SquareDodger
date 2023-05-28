namespace Project
{
    public interface IPoolerTarget
    {
        void PoppedFromPool();
        void PushedToPool();
    }
}