using UnityEngine;

namespace Project.Architecture
{
    public interface IPlayerBlendingShader
    {
        Material Material { get; set; }
        Color32 PlayerColor { get; set; }
        Color32 BlendingColor { get; set; }
        float BlendingRadius { get; set; }
        float BlendingLength { get; set; }
        float TotalBlendingLength { get; }
    }
}