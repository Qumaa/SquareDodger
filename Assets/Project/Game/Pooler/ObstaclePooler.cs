using System.Collections.Generic;

namespace Project.Game
{
    public abstract class Pooler<TTarget> : IPooler<TTarget>
        where TTarget : IPoolerTarget
    {
        private Stack<TTarget> _pool;
        
        public void Push(TTarget objToPool) =>
            _pool.Push(objToPool);

        public bool CanPop() =>
            _pool.Peek() != null;

        public TTarget Pop() =>
            _pool.Pop();
    }
}