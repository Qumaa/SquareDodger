using System;
using System.Collections.Generic;
using Project.Architecture;
using UnityEngine;

namespace Project.Game
{
    public interface IPlayerBlendingShaderMaintainer : IDisposable, IPausableAndResettable
    {
        void UpdateShader(List<IObstacle> data);
        IPlayerBlendingShader MaintainedShader { get; set; }
    }
}