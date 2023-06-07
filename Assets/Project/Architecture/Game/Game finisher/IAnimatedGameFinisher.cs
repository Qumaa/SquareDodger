using Project.Game;

namespace Project.Architecture
{
    public interface IAnimatedGameFinisher : IGameFinisher
    {
        IPlayer Player { get; set; }
        ICameraController CameraController { get; set; }
        IPlayerBlendingShader PlayerShader { get; set; }
    }
}