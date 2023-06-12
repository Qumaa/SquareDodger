namespace Project.Game
{
    public class PlayerShaderRuntimeData : ILoadableFrom<PlayerConfig>
    {
        public float BlendingRadius { get; private set; }
        public float BlendingLength { get; private set; }

        public void Load(PlayerConfig data)
        {
            BlendingRadius = data.BlendingRadius;
            BlendingLength = data.BlendingLength;
        }
    }
}