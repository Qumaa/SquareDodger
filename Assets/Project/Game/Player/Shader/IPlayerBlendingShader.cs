using UnityEngine;

namespace Project.Architecture
{
    public interface IPlayerBlendingShader
    {
        Material Material { get; set; }
        Color32 PlayerColor { get; set; }
        Color32 BlendingColor { get; set; }
        /// <summary>
        /// A [0, 1] range value where 0 = <see cref="PlayerColor"/> and 1 = <see cref="BlendingColor"/>>
        /// </summary>
        float ColorBalance { get; set; }
        float HardBlendingRadius { get; set; }
        float SoftBlendingLength { get; set; }
        float TotalBlendingRadius { get; }
    }
}