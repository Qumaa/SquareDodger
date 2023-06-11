using UnityEngine;

namespace Project.Architecture
{
    public interface IPlayerBlendingShader
    {
        Material Material { get; set; }
        Color32 PlayerColor { get; set; }
        Color32 BlendingColor { get; set; }
        float HardBlendingRadius { get; set; }
        float SoftBlendingLength { get; set; }
        float TotalBlendingRadius { get; }
    }
}