using System;
using System.Collections.Generic;

namespace Project.Architecture
{
    public class Disposer : IDisposer
    {
        private List<IDisposable> _disposables;

        public Disposer()
        {
            _disposables = new List<IDisposable>();
        }

        public void DisposeAll()
        {
            if (_disposables == null || _disposables.Count == 0)
                return;
            
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }

        public void Register(IDisposable disposable) =>
            _disposables.Add(disposable);
    }
}