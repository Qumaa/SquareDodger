using System;
using System.Collections.Generic;
using Project.Architecture;

namespace Project.Game
{
    public interface IBlendingShaderMaintainer : IDisposable, IPausableAndResettable
    {
        void UpdateShader(List<IObstacle> data);
        IBlendingShader MaintainedShader { get; }
    }
}