using Project.Game;
using Project.UI;

namespace Project.Architecture
{
    public interface IGame : IUpdatableAnFixedUpdatable, IUpdater, IFixedUpdater, IGameSettingsObserver
    {
        IGameplay Gameplay { get; set; }
        IGameCanvasUIRenderer UI { get; set; }
        ICameraController CameraController { get; set; }
        IGameInputService InputService { get; set; }
        IGameSounds GameSounds { get; set; }
        void Initialize();
    }
}