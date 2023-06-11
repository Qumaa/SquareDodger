using System;
using System.Collections.Generic;
using Project.Architecture;

namespace Project.Game
{
    public interface IPlayerBlendingShaderMaintainer : IDisposable, IPausableAndResettable
    {
        void UpdateShader(List<IObstacle> data);
        IPlayerBlendingShader MaintainedShader { get; set; }
    }
}