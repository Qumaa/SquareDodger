namespace Project.Game
{
    public interface IPlayerWithShader : IPlayer, IUpdatable
    {
        public IPlayerShaderMaintainer ShaderMaintainer { get; set; }
        public IObstacleManager ObstaclesSource { get; set; }
        public float TrailLength { get; set; }
    }
}