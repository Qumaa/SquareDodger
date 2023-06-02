using Project.UI;

namespace Project.Architecture
{
    public class PrefabGameLoader : IGameLoader
    {
        private IFactory<IGameplay> _gameFactory;
        private IFactory<IMainMenu> _menuFactory;

        public PrefabGameLoader(IFactory<IGameplay> gameFactory, IFactory<IMainMenu> menuFactory)
        {
            _gameFactory = gameFactory;
            _menuFactory = menuFactory;
        }

        public void Load(IGame game)
        {
            game.Gameplay = _gameFactory.CreateNew();
            game.MainMenu = _menuFactory.CreateNew();
            game.Initialize();
        }
    }
}