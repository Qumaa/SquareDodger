using Project.Game;

namespace Project.Architecture
{
    public interface IAnimatedGameFinisher : IGameFinisher, IResettable
    {
        IPlayer Player { get; set; }
        ICameraController CameraController { get; set; }
        IBlendingShader PlayerShader { get; set; }
    }
}