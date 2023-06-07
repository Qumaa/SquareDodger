using UnityEngine;

namespace Project.Architecture
{
    public interface IPlayerBlendingShader
    {
        Material Material { get; set; }
        float BlendingRadius { get; set; }
        float BlendingLength { get; set; }
    }
}