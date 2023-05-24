namespace Project.Game
{
    public interface IPoolerTarget
    {
        void PoppedFromPool();
        void PushedToPool();
    }
}