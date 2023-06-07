using System;
using Project.Architecture;
using UnityEngine;

namespace Project.Game
{
    public interface IPlayerBlendingShaderMaintainer : IDisposable, IPausableAndResettable
    {
        void UpdateShader(IObstacle[] data);
        IPlayerBlendingShader MaintainedShader { get; set; }
    }
}