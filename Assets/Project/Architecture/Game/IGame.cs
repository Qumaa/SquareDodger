using Project.Game;
using Project.UI;

namespace Project.Architecture
{
    public interface IGame : IUpdatableAnFixedUpdatable
    {
        IGameplay Gameplay { get; set; }
        IMainMenu MainMenu { get; set; }
        ICameraController CameraController { get; set; }
        void Run();
        void Initialize();
    }
}