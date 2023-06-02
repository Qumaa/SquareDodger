using UnityEngine;

namespace Project.Game
{
    public class GameCameraLogic : IGameCamera
    {
        private Vector2 _position;
        private ProceduralMotionSystemVector2 _motionSystem;
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

        public GameCameraLogic(Camera controlledCamera, float viewportDepth, ProceduralMotionSystemVector2 motionSystem,
            ICameraOffsetCalculator offsetCalculator, Transform followTarget, float bottomOffset)
        {
            _motionSystem = motionSystem;
            _offsetCalculator = offsetCalculator;
            _followTarget = followTarget;
            _controlledCamera = controlledCamera;
            _viewportDepth = viewportDepth;
            _bottomOffset = bottomOffset;
            
            _motionSystem.SetInitialValue(
                new ProceduralMotionSystemOperandVector2(Position),
                new ProceduralMotionSystemOperandVector2()
            );
            
            ResetPosition();
        }

        public void Update(float timeStep)
        {
            Position = _motionSystem
                .MakeStep(timeStep, new ProceduralMotionSystemOperandVector2(GetPositionWithOffset()))
                .Value;
        }

        private void SetPosition(Vector2 position)
        {
            _position = position;
            _controlledCamera.transform.position = new Vector3(_position.x, _position.y, -_viewportDepth);
        }

        private void ResetPosition() =>
            SetPosition(GetPositionWithOffset());

        private Vector2 GetPositionWithOffset() =>
            (Vector2)_followTarget.transform.position + _offsetCalculator.CalculateOffset(_bottomOffset);
    }
}