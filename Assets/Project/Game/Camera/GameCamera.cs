using UnityEngine;

namespace Project.Game
{
    public class GameCamera : PausableAndResettable, IGameCamera
    {
        private ProceduralMotionSystem<Vector2> _motionSystem;
        private ICameraOffsetCalculator _offsetCalculator;
        
        private float _bottomOffset;
        private float _defaultWidth;

        private Transform _target;

        public ICameraController CameraController { get; }

        public Transform Target
        {
            get => _target;
            set => SetTarget(value);
        }

        public Vector2 Position => CameraController.Position;

        public GameCamera(ICameraController cameraController, ProceduralMotionSystemVector2 motionSystem,
            ICameraOffsetCalculator offsetCalculator, float bottomOffset, float defaultWidth)
        {
            CameraController = cameraController;
            _motionSystem = motionSystem;
            _offsetCalculator = offsetCalculator;
            _bottomOffset = bottomOffset;
            _defaultWidth = defaultWidth;
        }

        public void FixedUpdate(float fixedTimeStep)
        {
            if (_isPaused)
                return;
            
            var position = _motionSystem
                .MakeStep(fixedTimeStep, new ProceduralMotionSystemOperandVector2(GetPositionWithOffset()))
                .Value;
            
            SetPosition(position);
        }
        
        public void ApplyTheme(IGameTheme theme) =>
            CameraController.ControlledCamera.backgroundColor = theme.BackgroundColor;

        protected override void OnReset()
        {
            ResetPosition();
            ResetWidth();
        }

        private void ResetPosition()
        {
            SetPosition(GetPositionWithOffset());
            
            _motionSystem.SetInitialValue(
                new ProceduralMotionSystemOperandVector2(CameraController.Position),
                new ProceduralMotionSystemOperandVector2()
            );
        }

        private void ResetWidth()
        {
            CameraController.WidthInUnits = _defaultWidth;
        }

        private void SetPosition(Vector2 position)
        {
            CameraController.Position = new Vector2(position.x, position.y);
        }

        private Vector2 GetPositionWithOffset() =>
            (Vector2)Target.transform.position + _offsetCalculator.CalculateOffset(_bottomOffset);

        private void SetTarget(Transform target)
        {
            _target = target;
            ResetPosition();
        }
    }
}