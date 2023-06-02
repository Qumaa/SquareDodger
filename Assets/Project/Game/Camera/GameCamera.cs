using UnityEngine;

namespace Project.Game
{
    public class GameCamera : PausableAndResettable, IGameCamera
    {
        private Vector2 _position;
        private ProceduralMotionSystem<Vector2> _motionSystem;
        private ICameraOffsetCalculator _offsetCalculator;

        private Transform _followTarget;
        private float _bottomOffset;

        private Camera _controlledCamera;
        private float _viewportDepth;

        public Vector2 Position
        {
            get => _position;
            set => SetPosition(value);
        }

        public Camera ControlledCamera => _controlledCamera;
        public Transform Target { get; set; }
        public float ViewportDepth => _viewportDepth;

        public GameCamera(Camera controlledCamera, float viewportDepth, ProceduralMotionSystemVector2 motionSystem,
            ICameraOffsetCalculator offsetCalculator, float bottomOffset)
        {
            _motionSystem = motionSystem;
            _offsetCalculator = offsetCalculator;
            _controlledCamera = controlledCamera;
            _viewportDepth = viewportDepth;
            _bottomOffset = bottomOffset;
        }

        public void FixedUpdate(float fixedTimeStep)
        {
            if (_isPaused)
                return;
            
            Position = _motionSystem
                .MakeStep(fixedTimeStep, new ProceduralMotionSystemOperandVector2(GetPositionWithOffset()))
                .Value;
        }

        protected override void OnReset()
        {
            ResetPosition();
        }

        private void ResetPosition()
        {
            SetPosition(GetPositionWithOffset());
            
            _motionSystem.SetInitialValue(
                new ProceduralMotionSystemOperandVector2(Position),
                new ProceduralMotionSystemOperandVector2()
            );
        }

        private void SetPosition(Vector2 position)
        {
            _position = position;
            _controlledCamera.transform.position = new Vector3(_position.x, _position.y, -_viewportDepth);
        }

        private Vector2 GetPositionWithOffset() =>
            (Vector2)Target.transform.position + _offsetCalculator.CalculateOffset(_bottomOffset);
    }
}