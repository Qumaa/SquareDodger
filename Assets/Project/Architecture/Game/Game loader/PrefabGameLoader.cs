using Project.UI;

namespace Project.Architecture
{
    public class PrefabGameLoader : IGameLoader
    {
        private IFactory<IGameplay> _gameFactory;
        private IFactory<IGameCanvasUIRenderer> _uiFactory;
        
        public PrefabGameLoader(IFactory<IGameplay> gameFactory, IFactory<IGameCanvasUIRenderer> uiFactory)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Load(IGame game)
        {
            game.Gameplay = _gameFactory.CreateNew();
            game.GameCanvasUI = _uiFactory.CreateNew();
        }
    }
}