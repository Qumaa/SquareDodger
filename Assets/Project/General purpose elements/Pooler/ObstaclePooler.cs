using System.Collections.Generic;

namespace Project
{
    public abstract class Pooler<TTarget> : IPooler<TTarget>
        where TTarget : IPoolerTarget
    {
        private Stack<TTarget> _pool;

        public Pooler()
        {
            _pool = new Stack<TTarget>();
        }

        public void Push(TTarget objToPool)
        {
            _pool.Push(objToPool);
            objToPool.PushedToPool();
        }

        public bool CanPop() =>
            _pool.TryPeek(out var res) && res != null;

        public TTarget Pop()
        {
            var popped = _pool.Pop();
            popped.PoppedFromPool();
            return popped;
        }
    }
}