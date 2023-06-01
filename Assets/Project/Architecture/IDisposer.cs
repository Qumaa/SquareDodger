using System;

namespace Project.Architecture
{
    public interface IDisposer
    {
        void DisposeAll();
        void Register(IDisposable disposable);
    }
}