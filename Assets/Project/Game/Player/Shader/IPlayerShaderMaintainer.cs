using System;
using Project.Architecture;
using UnityEngine;

namespace Project.Game
{
    public interface IPlayerShaderMaintainer : IDisposable, IPausableAndResettable
    {
        void UpdateBuffer(IObstacle[] data);
        Material Material { get; set; }
        float BlendingRadius { get; }
    }
}