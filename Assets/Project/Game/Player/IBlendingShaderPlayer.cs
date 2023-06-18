namespace Project.Game
{
    public interface IBlendingShaderPlayer : IPlayer, IUpdatable
    {
        public IPlayerBlendingShaderMaintainer ShaderMaintainer { get; set; }
        public IObstacleManager ObstaclesSource { get; set; }
    }
}