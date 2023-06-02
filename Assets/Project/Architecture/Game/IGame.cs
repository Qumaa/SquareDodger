using Project.UI;

namespace Project.Architecture
{
    public interface IGame : IUpdatableAnFixedUpdatable
    {
        IGameplay Gameplay { get; set; }
        IMainMenu MainMenu { get; set; }
        void Initialize();
        void Run();
    }
}