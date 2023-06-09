using Project.Game;

namespace Project.Architecture
{
    public struct GameCameraFactory : IFactory<IGameCamera>
    {
        private GameCameraRuntimeData _cameraData;
        private ICameraController _cameraController;

        public GameCameraFactory(GameCameraRuntimeData cameraData, ICameraController cameraController)
        {
            _cameraData = cameraData;
            _cameraController = cameraController;
        }

        public IGameCamera CreateNew()
        {
            var gameCamera = new GameCamera(
                _cameraController,
                new ProceduralMotionSystemVector2(_cameraData.MotionSpeed, _cameraData.MotionDamping,
                    _cameraData.MotionResponsiveness),
                new CameraOffsetCalculatorViewport(_cameraController.ControlledCamera),
                _cameraData.BottomOffset
            );

            return gameCamera;
        }
    }
}