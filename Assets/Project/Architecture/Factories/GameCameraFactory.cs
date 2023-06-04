using Project.Game;

namespace Project.Architecture
{
    public struct GameCameraFactory : IFactory<IGameCamera>
    {
        private GameCameraConfig _cameraConfig;
        private ICameraController _cameraController;

        public GameCameraFactory(GameCameraConfig cameraConfig, ICameraController cameraController)
        {
            _cameraConfig = cameraConfig;
            _cameraController = cameraController;
        }

        public IGameCamera CreateNew()
        {
            var gameCamera = new GameCamera(
                _cameraController,
                new ProceduralMotionSystemVector2(_cameraConfig.MotionSpeed, _cameraConfig.MotionDamping,
                    _cameraConfig.MotionResponsiveness),
                new CameraOffsetCalculatorViewport(_cameraController.ControlledCamera),
                _cameraConfig.BottomOffset
            );

            return gameCamera;
        }
    }
}