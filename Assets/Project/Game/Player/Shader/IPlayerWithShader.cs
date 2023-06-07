namespace Project.Game
{
    public interface IPlayerWithShader : IPlayer, IUpdatable
    {
        public IPlayerBlendingShaderMaintainer ShaderMaintainer { get; set; }
        public IObstacleManager ObstaclesSource { get; set; }
    }
}