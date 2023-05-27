namespace Project.Game
{
    public static class PoolerExtensions
    {
        public static bool TryPop<T>(this IPooler<T> pooler, out T obj)
            where T : IPoolerTarget
        {
            if (pooler.CanPop())
            {
                obj = pooler.Pop();
                return true;
            }

            obj = default;
            return false;
        }
    }
}