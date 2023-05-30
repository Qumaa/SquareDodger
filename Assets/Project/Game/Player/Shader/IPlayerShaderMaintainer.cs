using System;
using UnityEngine;

namespace Project.Game
{
    public interface IPlayerShaderMaintainer : IDisposable
    {
        void UpdateBuffer(IObstacle[] data);
        Material Material { get; set; }
        float BlendingRadius { get; }
    }
}