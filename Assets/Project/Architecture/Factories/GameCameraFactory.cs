using Project.Game;
using UnityEngine;

namespace Project.Architecture
{
    public struct GameCameraFactory : IFactory<IGameCamera>
    {
        private GameCameraConfig _cameraConfig;
        private Camera _controlledCamera;

        public GameCameraFactory(GameCameraConfig cameraConfig, Camera controlledCamera)
        {
            _cameraConfig = cameraConfig;
            _controlledCamera = controlledCamera;
        }

        public IGameCamera CreateNew()
        {
            var gameCamera = new GameCamera(
                _controlledCamera,
                _cameraConfig.ViewportDepth,
                new ProceduralMotionSystemVector2(_cameraConfig.MotionSpeed, _cameraConfig.MotionDamping,
                    _cameraConfig.MotionResponsiveness),
                new CameraOffsetCalculatorViewport(_controlledCamera),
                _cameraConfig.BottomOffset
            );

            return gameCamera;
        }
    }
}