namespace Project.Game
{
    public interface IBlendingShaderPlayer : IPlayer, IUpdatable
    {
        public IBlendingShaderMaintainer PlayerShaderMaintainer { set; }
        public IBlendingShaderMaintainer TrailShaderMaintainer { set; }
        public IObstacleManager ObstaclesSource { get; set; }
        void SetShaderMode(ShaderBlendingMode mode);
    }
}