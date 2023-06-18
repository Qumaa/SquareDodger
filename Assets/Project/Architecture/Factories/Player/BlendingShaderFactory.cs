namespace Project.Architecture
{
    public struct BlendingShaderFactory : IFactory<IBlendingShader>
    {
        private float _blendingRadius;
        private float _blendingLength;

        public BlendingShaderFactory(float blendingRadius, float blendingLength)
        {
            _blendingRadius = blendingRadius;
            _blendingLength = blendingLength;
        }

        public IBlendingShader CreateNew()
        {
            var shader = new PlayerBlendingShader
            {
                HardBlendingRadius = _blendingRadius,
                SoftBlendingLength = _blendingLength,
            };
            
            return shader;
        }
    }
}