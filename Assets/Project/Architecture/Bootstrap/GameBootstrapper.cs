using UnityEngine;
using Project.Game;

namespace Project.Architecture
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        
        private IGame _game;
        private IDisposer _disposer;

        private void Start()
        {
            InitializeDisposer();
            InitializeGame();
        }

        private void FixedUpdate()
        {
            _game.FixedUpdate();
        }

        private void Update()
        {
            _game.Update();
        }

        private void InitializeDisposer()
        {
            _disposer = new Disposer();
        }

        private void InitializeGame()
        {
            var controlledCamera = Camera.main;
            _game = new PausedGameFactory(controlledCamera, _gameConfig.CameraConfig, _gameConfig.ManagerConfig,
                    _gameConfig.PlayerConfig, _disposer)
                .CreateNew();
        }

        private void OnDestroy()
        {
            _disposer?.DisposeAll();
        }
    }
}
