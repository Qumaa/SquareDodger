using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;

        private IGame _game;
        private IDisposer _disposer;

        private void Awake()
        {
            var controlledCamera = Camera.main;
            _disposer = new Disposer();

            CreateGame(controlledCamera);
        }

        private void CreateGame(Camera controlledCamera)
        {
            var resolver = new ResourcesGameThemeResolver();
            var gameData = new GameRuntimeData();
            gameData.Load(_gameConfig);

            _game = new Game(gameData, controlledCamera, _disposer, resolver);
            _game.InputService = GetComponent<IGameInputService>();
            _game.Initialize();
        }

        private void Update()
        {
            _game.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _game.FixedUpdate(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _disposer.DisposeAll();
        }
    }
}