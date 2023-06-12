namespace Project.Architecture
{
    public struct BlendingShaderFactory : IFactory<IPlayerBlendingShader>
    {
        private float _blendingRadius;
        private float _blendingLength;

        public BlendingShaderFactory(float blendingRadius, float blendingLength)
        {
            _blendingRadius = blendingRadius;
            _blendingLength = blendingLength;
        }

        public IPlayerBlendingShader CreateNew()
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