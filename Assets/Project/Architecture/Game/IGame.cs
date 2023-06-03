using Project.UI;

namespace Project.Architecture
{
    public interface IGame : IUpdatableAnFixedUpdatable
    {
        IGameplay Gameplay { get; set; }
        IMainMenu MainMenu { get; set; }
        IGameLoader GameLoader { get; set; }
        void Run();
        void Initialize();
    }
}