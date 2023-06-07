using UnityEngine;

namespace Project.Architecture
{
    public class PlayerBlendingShader : IPlayerBlendingShader
    {
        private const string _MATERIAL_BLENDING_RADIUS_NAME = "_BlendingRadius";
        private const string _MATERIAL_BLENDING_WIDTH_NAME = "_BlendingLength";
        private readonly int _blendingRadiusNameId = Shader.PropertyToID(_MATERIAL_BLENDING_RADIUS_NAME);
        private readonly int _blendingLengthNameId = Shader.PropertyToID(_MATERIAL_BLENDING_WIDTH_NAME);

        public Material Material { get; set; }
        
        public float BlendingRadius
        {
            get => Material.GetFloat(_blendingRadiusNameId);
            set => Material.SetFloat(_blendingRadiusNameId, value);
        }

        public float BlendingLength
        {
            get => Material.GetFloat(_blendingLengthNameId);
            set => Material.SetFloat(_blendingLengthNameId, value);
        }
    }
}